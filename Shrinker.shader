Shader "Unlit/Shrinker"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
           
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
               
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
            
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float3 _globopos;
            float _diver;

            v2f vert (appdata v)
            {
                v2f o;
                float3 glob = mul(unity_ObjectToWorld,v.vertex );
                float thing =abs(length(glob-_globopos))/2;
                //thing = clamp(thing,0,1);
                float t= (thing-1)/(_diver-1 );
                if (t < -1)
                {
                    t= -1;
                }
                else if ( t >1)
                {
                    t=1;
                    
                }
                float anotherthing = lerp(-1,0, t*t*t*t*t);
                 if (anotherthing < -1)
                {
                    anotherthing= -1;
                }
                else if ( anotherthing >0)
                {
                    anotherthing=0;
                    
                }
                v.vertex.xyz += v.vertex.xyz*anotherthing;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
               
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
               
                return col;
            }
            ENDCG
        }
    }
}
