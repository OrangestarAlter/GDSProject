Shader "Unlit/Circle"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Speed("speed", Float) = 1
    }
    SubShader
    {
        Cull Off ZWrite Off ZTest Always
                // 开启透明度
        Blend SrcAlpha OneMinusSrcAlpha
        // 设置渲染队列
        Tags { "Queue" = "Transparent" "RenderType" = "Opaque" }

        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Speed;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float disToCenter = (i.uv.x - 0.5) * (i.uv.x - 0.5) + (i.uv.y - 0.5) * (i.uv.y - 0.5);
                float radius = sin(_Speed - floor(_Speed)) * 0.2;
                float dis2 = disToCenter - radius;
                if (0.35 * 0.35 > dis2 && dis2 > 0.3 * 0.3) {
                    float alpha = dis2 - 0.1;
                    return fixed4(1, 0, 0, alpha) * 3.0;
                }
                else if(0.25 * 0.25 > dis2 && dis2 > 0.05 * 0.05)
                    return fixed4(1 ,0, 0, dis2 * (1-radius) * (1-radius)) * 3.0;
                else
                    return fixed4(1, 1, 1, 0);
            }
            ENDCG
        }
    }
}
