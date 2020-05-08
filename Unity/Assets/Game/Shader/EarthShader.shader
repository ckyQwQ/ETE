Shader "Custom/EarthShader"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("MainTex", 2D) = "white" {}
        _DayTex("DayTex", 2D) = "white" {}
        _NightTex("NightTex", 2D) = "white" {}
        _BumpMap("Normal Map", 2D) = "bump" {}
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" "Queue" = "Geometry"}
        Pass {
            Tags { "LightMode" = "ForwardBase" }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "AutoLight.cginc"

            fixed4 _Color;
            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _DayTex;
            float4 _DayTex_ST;
            sampler2D _NightTex;
            float4 _NightTex_ST;
            sampler2D _BumpMap;
            float4 _BumpMap_ST;


            struct a2v {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float4 texcoord : TEXCOORD0;
            };

            struct v2f {
                float4 pos : POSITION;
                float4 uv : TEXCOORD0;
                float4 uv_day_night : TEXCOORD1;
                float4 TtoW0 : TEXCOORD2;
                float4 TtoW1 : TEXCOORD3;
                float4 TtoW2 : TEXCOORD4;
            };

            v2f vert(a2v v) {
                v2f o;

                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv.xy = TRANSFORM_TEX(v.texcoord, _MainTex);
                o.uv.zw = v.texcoord.xy * _BumpMap_ST.xy + _BumpMap_ST.zw;
                o.uv_day_night.xy = TRANSFORM_TEX(v.texcoord, _DayTex);
                o.uv_day_night.zw = TRANSFORM_TEX(v.texcoord, _NightTex);

                float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                fixed3 worldNormal = UnityObjectToWorldNormal(v.normal);
                fixed3 worldTangent = UnityObjectToWorldDir(v.tangent.xyz);
                fixed3 worldBinormal = cross(worldNormal, worldTangent) * v.tangent.w;

                o.TtoW0 = float4(worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x);
                o.TtoW1 = float4(worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y);
                o.TtoW2 = float4(worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z);

                return o;
            }

            float4 frag(v2f i) : SV_Target{
                float2 speed = float2(_Time.y * 0.005, 0);

                fixed3 cloud = tex2D(_MainTex, i.uv.xy + speed).rgb;
                fixed3 albedo = tex2D(_DayTex, i.uv_day_night.xy).rgb;
                fixed3 night_albedo = tex2D(_NightTex, i.uv_day_night.zw).rgb;
                float3 worldPos = float3(i.TtoW0.w, i.TtoW1.w, i.TtoW2.w);
                fixed3 lightDir = normalize(UnityWorldSpaceLightDir(worldPos));

                fixed3 bump = UnpackNormal(tex2D(_BumpMap, i.uv.zw));
                bump = normalize(half3(dot(i.TtoW0.xyz, bump), dot(i.TtoW1.xyz, bump), dot(i.TtoW2.xyz, bump)));
                fixed3 diffuse = _LightColor0.rgb * albedo * max(0, dot(bump, lightDir));
                fixed3 cloud_diffuse = _LightColor0.rgb * cloud * max(0, dot(bump, lightDir));
                return fixed4(diffuse + night_albedo + cloud_diffuse, 1.0);
            }

            ENDCG
        }
    }
    FallBack "Diffuse"
}
