﻿Shader "Billboard/Billboard" {
		Properties{
		//[PerRendererData]
		_MainTex("Main Tex", 2D) = "white" {}
		//[PerRendererData]
		_Color("Color Tint", Color) = (1, 1, 1, 1)
	
		[Toggle(_ROTATE_BASE_UV1)] _RotateBaseuv1("Useuv1AsRotateBase?", Int) = 0
	
	}
		SubShader{
			Tags 
			{
				// "Queue" = "Transparent"
				// "RenderType" = "Transparent"
                
                "Queue" = "Geometry"
                "RenderType" = "Opaque"
		
				// "DisableBatching" = "True"
		
			}

			Pass {

				// ZWrite Off
                
                ZWrite On

                
				// Blend SrcAlpha OneMinusSrcAlpha
				Cull Off

				CGPROGRAM

				#pragma vertex vert
				#pragma fragment frag
				
				#pragma multi_compile_instancing

			
				#pragma shader_feature _ROTATE_BASE_UV1
				// #pragma multi_compile __ ROTATE_BASE_UV1

				#include "Lighting.cginc"

				sampler2D _MainTex;
				float4 _MainTex_ST;
				fixed4 _Color;

				struct a2v {
					float4 vertex : POSITION;
					float4 texcoord : TEXCOORD0;
					float4 worldOffset : TEXCOORD1;

					UNITY_VERTEX_INPUT_INSTANCE_ID

				};

				struct v2f {
					float4 pos : SV_POSITION;
					float2 uv : TEXCOORD0;

					UNITY_VERTEX_INPUT_INSTANCE_ID
				};

				//UNITY_INSTANCING_BUFFER_START(Props)

				//UNITY_INSTANCING_BUFFER_END(Props)

				v2f vert(a2v v) {
                
					v2f o;

					UNITY_SETUP_INSTANCE_ID(v);

					float3 rotateBase = float3(0, 0, 0);
#ifdef _ROTATE_BASE_UV1
					rotateBase += v.worldOffset;
#endif
					// in local space
					float3 camPos = mul(unity_WorldToObject,float4(_WorldSpaceCameraPos, 1));

					float3 normalDir = camPos - rotateBase;
					// normalDir.y = 0;
					float3 forwardDir = normalize(normalDir);
                    
					float3 upDir = float3(0, 1, 0);

					float3 rightDir = normalize(cross(upDir, forwardDir));
					upDir = normalize(cross(forwardDir, rightDir));

					// absolute offset
					float3 centerOffs = v.vertex.xyz - rotateBase;
					float3 localPos = 
						rotateBase +
						rightDir * centerOffs.x + 
						upDir * centerOffs.y + 
						forwardDir * centerOffs.z;

					o.pos = UnityObjectToClipPos(float4(localPos, 1));
					o.uv = TRANSFORM_TEX(v.texcoord,_MainTex);

					return o;
				}

				fixed4 frag(v2f i) : SV_Target {
					fixed4 c = tex2D(_MainTex, i.uv);
					c.rgb *= _Color.rgb;
                    
                    
                    // alpha test
                    clip (c.a - 0.5);

                    
					return c;
				}

				ENDCG
			}
		}
		FallBack "Transparent/VertexLit"
}