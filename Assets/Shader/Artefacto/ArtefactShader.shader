Shader "Custom/ArtefactShader"
{
    Properties
    {
        _MainTex("Main Texture", 2D) = "defaulttexture" {}
       // _iChannelTex("iChannel Texture", 2D) = "defaulttexture"{}
    }

    SubShader
    {
     Cull off
        CGPROGRAM
            #pragma surface surf Lambert 
            #define FAR (2)

        int id = 0; // Object ID - Red perspex: 0; Black lattice: 1.

        // Tri-Planar blending function. Based on an old Nvidia writeup:
        // GPU Gems 3 - Ryan Geiss: https://developer.nvidia.com/gpugems/GPUGems3/gpugems3_ch01.html
        float3 tex3D(sampler2D tex, in float3 p, in float3 n) 
        {
          
           n = max((abs(n) - .2), .001);
           n /= (n.x + n.y + n.z); // Roughly normalized.
          
           p = (tex2D(tex, p.yz) * n.x + tex2D(tex, p.zx) * n.y + tex2D(tex, p.xy) * n.z).xyz;
          
           // Loose sRGB to RGB conversion to counter final value gamma correction...
           // in case you're wondering.
           return p * p;
        }

        // Compact, self-contained version of IQ's 3D value noise function. I have a transparent noise
        // example that explains it, if you require it.
        float n3D(float3 p) {

            const float3 s = float3(7, 157, 113);
            float3 ip = floor(p); p -= ip;
            float4 h = float4(0., s.yz, s.y + s.z) + dot(ip, s);
            p = p * p * (3. - 2. * p); //p *= p*p*(p*(p * 6. - 15.) + 10.);
            h = lerp(frac(sin(h) * 43758.5453), frac(sin(h + s.x) * 43758.5453), p.x);
            h.xy = lerp(h.xz, h.yw, p.y);
            return lerp(h.x, h.y, p.z); // Range: [0, 1].
        }

        // vec2 to vec2 hash.
        float2 hash22(float2 p) {

            // Faster, but doesn't disperse things quite as nicely. However, when framerate
            // is an issue, and it often is, this is a good one to use. Basically, it's a tweaked 
            // amalgamation I put together, based on a couple of other random algorithms I've 
            // seen around... so use it with caution, because I make a tonne of mistakes. :)
            float n = sin(dot(p, float2(41, 289)));
            //return fract(vec2(262144, 32768)*n); 

            // Animated.
            p = frac(float2(262144, 32768) * n);
            // Note the ".45," insted of ".5" that you'd expect to see. When edging, it can open 
            // up the cells ever so slightly for a more even spread. In fact, lower numbers work 
            // even better, but then the random movement would become too restricted. Zero would 
            // give you square cells.
            return sin(p * 6.2831853 + _Time) * .45 + .5;

        }

        // 2D 2nd-order Voronoi: Obviously, this is just a rehash of IQ's original. I've tidied
        // up those if-statements. Since there's less writing, it should go faster. That's how 
        // it works, right? :)
        //
        float Voronoi(in float2 p) {

            float2 g = floor(p), o; p -= g;

            float3 d = float3(1,1,1); // 1.4, etc. "d.z" holds the distance comparison value.

            for (int y = -1; y <= 1; y++) {
                for (int x = -1; x <= 1; x++) {

                    o = float2(x, y);
                    o += hash22(g + o) - p;     //PERGUNTAR AO PROFESSOR EM CASO DE ERRO

                    d.z = dot(o, o);
                    // More distance metrics.
                    //o = abs(o);
                    //d.z = max(o.x*.8666 + o.y*.5, o.y);// 
                    //d.z = max(o.x, o.y);
                    //d.z = (o.x*.7 + o.y*.7);

                    d.y = max(d.x, min(d.y, d.z));
                    d.x = min(d.x, d.z);

                }
            }

            return max(d.y / 1.2 - d.x * 1., 0.) / 1.2;
            //return d.y - d.x; // return 1.-d.x; // etc.

        }

        // The height map values. In this case, it's just a Voronoi variation. By the way, I could
        // optimize this a lot further, but it's not a particularly taxing distance function, so
        // I've left it in a more readable state.
        float heightMap(float3 p) {

            id = 0;
            float c = Voronoi(p.xy * 4.); // The fiery bit.

            // For lower values, reverse the surface direction, smooth, then
            // give it an ID value of one. Ie: this is the black web-like
            // portion of the surface.
            if (c < .07) { c = smoothstep(0.7, 1., 1. - c) * .2; id = 1; }

            return c;
        }

        // Standard back plane height map. Put the plane at vec3(0, 0, 1), then add some height values.
        // Obviously, you don't want the values to be too large. The one's here account for about 10%
        // of the distance between the plane and the camera.
        float m(float3 p) 
        {

            float h = heightMap(p); // texture(iChannel0, p.xy/2.).x; // Texture work too.

            return 1. - p.z - h * .1;

        }

        // The normal function with some edge detection rolled into it.
        float3 nr(float3 p, inout float edge) {

            float2 e = float2(.005, 0);

            // Take some distance function measurements from either side of the hit point on all three axes.
            float d1 = m(p + e.xyy), d2 = m(p - e.xyy);
            float d3 = m(p + e.yxy), d4 = m(p - e.yxy);
            float d5 = m(p + e.yyx), d6 = m(p - e.yyx);
            float d = m(p) * 2.;	// The hit point itself - Doubled to cut down on calculations. See below.

            // Edges - Take a geometry measurement from either side of the hit point. Average them, then see how
            // much the value differs from the hit point itself. Do this for X, Y and Z directions. Here, the sum
            // is used for the overall difference, but there are other ways. Note that it's mainly sharp surface 
            // curves that register a discernible difference.
            edge = abs(d1 + d2 - d) + abs(d3 + d4 - d) + abs(d5 + d6 - d);
            //edge = max(max(abs(d1 + d2 - d), abs(d3 + d4 - d)), abs(d5 + d6 - d)); // Etc.

            // Once you have an edge value, it needs to normalized, and smoothed if possible. How you 
            // do that is up to you. This is what I came up with for now, but I might tweak it later.
            edge = smoothstep(0., 1., sqrt(edge / e.x * 2.));

            // Return the normal.
            // Standard, normalized gradient mearsurement.
            return normalize(float3(d1 - d2, d3 - d4, d5 - d6));
        }

        float3 eMap(float3 rd, float3 sn) {

            float3 sRd = rd; // Save rd, just for some mixing at the end.

            // Add a time component, scale, then pass into the noise function.
            rd.xy -= _Time * .25;
            rd *= 3.;

            //vec3 tx = tex3D(iChannel0, rd/3., sn);
            //float c = dot(tx*tx, vec3(.299, .587, .114));

            float c = n3D(rd) * .57 + n3D(rd * 2.) * .28 + n3D(rd * 4.) * .15; // Noise value.
            c = smoothstep(0.5, 1., c); // Darken and add contast for more of a spotlight look.

            //vec3 col = vec3(c, c*c, c*c*c*c).zyx; // Simple, warm coloring.
            float3 col = float3(min(c * 1.5, 1.), pow(c, 2.5), pow(c, 12.)).zyx; // More color.

            // Mix in some more red to tone it down and return.
            return lerp(col, col.yzx, sRd * .25 + .25);

        }

        // Vignette.
        //vec2 uv = u/iResolution.xy;
        //c.xyz = mix(c.xyz, vec3(0, 0, .5), .1 -pow(16.*uv.x*uv.y*(1.-uv.x)*(1.-uv.y), 0.25)*.1);

        // Apply some statistically unlikely (but close enough) 2.0 gamma correction. :)
        //c = vec4(sqrt(clamp(c.xyz, 0., 1.)), 1.);




        struct Input 
        {
        float2 uv_MainTex;
        };


        sampler2D _MainTex;
        //sampler2D _iChannelTex;


        void surf(Input IN, inout SurfaceOutput o)//, inout float2 u, inout float4 c )
        {
            
            // Unit direction ray, camera origin and light position.
            float3 r = normalize(float3(IN.uv_MainTex - 0.5, 1)),
                    qqc = float3(0,0,0), l = qqc + float3(0, 0, -1);

            // Rotate the canvas. Note that sine and cosine are kind of rolled into one.
            float2 a = sin(float2(1.570796, 0) + _Time / 8.); // Fabrice's observation.
            r.xy = mul (float2x2(a, -a.y, a.x) , r.xy);


            // Standard raymarching routine. Raymarching a slightly perturbed back plane front-on
            // doesn't usually require many iterations. Unless you rely on your GPU for warmth,
            // this is a good thing. :)
            float d;
            float t = 0.0;

            for (int i = 0; i < 32; i++) {

                d = m(qqc + r * t);
                // There isn't really a far plane to go beyond, but it's there anyway.
                if (abs(d) < 0.001 || t > FAR) break;
                t += d * .7;

            }

            t = min(t, FAR);

            // Set the initial scene color to black.
            o.Emission = 0;

            float edge = 0.; // Edge value - to be passed into the normal.

            if (t < FAR)
            {

                float3 p = qqc + r * t, n = nr(p, edge);

                l -= p; // Light to surface vector. Ie: Light direction vector.
                d = max(length(l), 0.001); // Light to surface distance.
                l /= d; // Normalizing the light direction vector.



                // Obtain the height map (destorted Voronoi) value, and use it to slightly
                // shade the surface. Gives a more shadowy appearance.
                float hm = heightMap(p);

                // Texture value at the surface. Use the heighmap value above to distort the
                // texture a bit.
                float3 tx = tex3D(_MainTex, (p * 2. + hm * .2), n);   //<----------------------------- USA iCHANNEL. QUANDO ENCONTRAR PACIENCIENCIA PARA MANUALMENTE ADICIONAR ESTA FUNÇÂO TALVEZ O FAÇA. DESCOMENTAR A LINHA NESSE CASO
                //tx = floor(tx*15.999)/15.; // Quantized cartoony colors, if you get bored enough.

                o.Emission = float3(1, 1, 1) * (hm * .8 + .2); // Applying the shading to the final color.

                o.Emission *= float3(1.5,1.5,1.5) * tx ; // Multiplying by the texture value and lightening.


                // Color the cell part with a fiery (I incorrectly spell it firey all the time) 
                // palette and the latticey web thing a very dark color.
                //
                o.Emission = (dot(o.Emission, float3(.299, .587, .114))).x; // Grayscale.
                if (id == 0) o.Emission *= float3(min(o.Emission.x * 1.5, 1.), pow(o.Emission.x, 5.), pow(o.Emission.x, 24.)) * 2.;
                else o.Emission *= .1;

                // Hue rotation, for anyone who's interested.
                //c.xyz = rotHue(c.xyz, mod(_Time/16., 6.283));


                float df = max(dot(l, n), 0.); // Diffuse.
                float sp = pow(max(dot(reflect(-l, n), -r), 0.), 32.); // Specular.

                if (id == 1) sp *= sp; // Increase specularity on the dark lattice.

                // Applying some diffuse and specular lighting to the surface.
                o.Emission = o.Emission * (df + .75) + float3(1, .97, .92) * sp + float3(.5, .7, 1) * pow(sp, 32.);

                // Add the fake environmapping. Give the dark surface less reflectivity.
                float3 em = eMap(reflect(r, n), n); // Fake environment mapping.
                if (id == 1) em *= .5;
                o.Emission += em;

                // Edges.
                //if(id == 0)c.xyz += edge*.1; // Lighter edges.
                o.Emission *= 1. - edge * .8; // Darker edges.

                // Attenuation, based on light to surface distance.    
                o.Emission *= 1. / (1. + d * d * .125);

                // AO - The effect is probably too subtle, in this case, so we may as well
                // save some cycles.
                //c.xyz *= cAO(p, n);

            }
        }

        ENDCG
    }
   FallBack "Diffuse"
   }