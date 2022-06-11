// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Hidden/Mtree/SRP/VertexColorShader LWRP"
{
    Properties
    {
		[HideInInspector] _AlphaCutoff("Alpha Cutoff ", Range(0, 1)) = 0.5
		[HideInInspector] _EmissionColor("Emission Color", Color) = (1,1,1,1)
		[Header(Wind)]_GlobalWindInfluence("Global Wind Influence", Range( 0 , 1)) = 1
		[Enum(Off,0,Front,1,Back,2)]_CullMode("Cull Mode", Int) = 2

    }


    SubShader
    {
		LOD 0

		
        Tags { "RenderPipeline"="LightweightPipeline" "RenderType"="Opaque" "Queue"="Geometry" }

		Cull [_CullMode]
		HLSLINCLUDE
		#pragma target 3.0
		ENDHLSL

		
        Pass
        {
			
        	Tags { "LightMode"="LightweightForward" }

        	Name "Base"
			Blend One Zero , One Zero
			ZWrite On
			ZTest LEqual
			Offset 0 , 0
			ColorMask RGBA
            

        	HLSLPROGRAM
            #pragma multi_compile _ LOD_FADE_CROSSFADE
            #pragma multi_compile_fog
            #define ASE_FOG 1
            #define ASE_ABSOLUTE_VERTEX_POS 1
            #define ASE_SRP_VERSION 60902

            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x
            
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
            #pragma multi_compile _ _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS
            #pragma multi_compile _ _ADDITIONAL_LIGHT_SHADOWS
            #pragma multi_compile _ _SHADOWS_SOFT
            #pragma multi_compile _ _MIXED_LIGHTING_SUBTRACTIVE
            
            #pragma multi_compile _ DIRLIGHTMAP_COMBINED
            #pragma multi_compile _ LIGHTMAP_ON
            #pragma multi_compile_fog

            #pragma multi_compile_instancing

            #pragma vertex vert
        	#pragma fragment frag

        	#include "Packages/com.unity.render-pipelines.lightweight/ShaderLibrary/Core.hlsl"
        	#include "Packages/com.unity.render-pipelines.lightweight/ShaderLibrary/Lighting.hlsl"
        	#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        	#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/UnityInstancing.hlsl"
        	#include "Packages/com.unity.render-pipelines.lightweight/ShaderLibrary/ShaderGraphFunctions.hlsl"
		
			#define ASE_NEEDS_VERT_POSITION


			float _WindStrength;
			float _RandomWindOffset;
			float _WindPulse;
			float _WindDirection;
			CBUFFER_START( UnityPerMaterial )
			int _CullMode;
			float _GlobalWindInfluence;
			CBUFFER_END


            struct GraphVertexInput
            {
                float4 vertex : POSITION;
                float3 ase_normal : NORMAL;
                float4 ase_tangent : TANGENT;
                float4 texcoord1 : TEXCOORD1;
				float4 ase_color : COLOR;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

        	struct GraphVertexOutput
            {
                float4 clipPos                : SV_POSITION;
                float4 lightmapUVOrVertexSH	  : TEXCOORD0;
        		half4 fogFactorAndVertexLight : TEXCOORD1;
            	float4 shadowCoord            : TEXCOORD2;
				float4 tSpace0					: TEXCOORD3;
				float4 tSpace1					: TEXCOORD4;
				float4 tSpace2					: TEXCOORD5;
				float4 ase_color : COLOR;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            	UNITY_VERTEX_OUTPUT_STEREO
            };

			float2 DirectionalEquation( float _WindDirection )
			{
				float d = _WindDirection * 0.0174532924;
				float xL = cos(d) + 1 / 2;
				float zL = sin(d) + 1 / 2;
				return float2(zL,xL);
			}
			

            GraphVertexOutput vert (GraphVertexInput v  )
        	{
        		GraphVertexOutput o = (GraphVertexOutput)0;
                UNITY_SETUP_INSTANCE_ID(v);
            	UNITY_TRANSFER_INSTANCE_ID(v, o);
        		UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				float3 VAR_VertexPosition21_g2 = mul( GetObjectToWorldMatrix(), float4( v.vertex.xyz , 0.0 ) ).xyz;
				float3 break109_g2 = VAR_VertexPosition21_g2;
				float VAR_WindStrength43_g2 = ( _WindStrength * _GlobalWindInfluence );
				float4 transform37_g2 = mul(GetObjectToWorldMatrix(),float4( 0,0,0,1 ));
				float2 appendResult38_g2 = (float2(transform37_g2.x , transform37_g2.z));
				float dotResult2_g3 = dot( appendResult38_g2 , float2( 12.9898,78.233 ) );
				float lerpResult8_g3 = lerp( 0.8 , ( ( _RandomWindOffset / 2.0 ) + 0.9 ) , frac( ( sin( dotResult2_g3 ) * 43758.55 ) ));
				float VAR_RandomTime16_g2 = ( ( _TimeParameters.x * 0.05 ) * lerpResult8_g3 );
				float FUNC_Turbulence36_g2 = ( sin( ( ( VAR_RandomTime16_g2 * 40.0 ) - ( VAR_VertexPosition21_g2.z / 15.0 ) ) ) * 0.5 );
				float VAR_WindPulse274_g2 = _WindPulse;
				float FUNC_Angle73_g2 = ( VAR_WindStrength43_g2 * ( 1.0 + sin( ( ( ( ( VAR_RandomTime16_g2 * 2.0 ) + FUNC_Turbulence36_g2 ) - ( VAR_VertexPosition21_g2.z / 50.0 ) ) - ( v.ase_color.r / 20.0 ) ) ) ) * sqrt( v.ase_color.r ) * 0.2 * VAR_WindPulse274_g2 );
				float VAR_SinA80_g2 = sin( FUNC_Angle73_g2 );
				float VAR_CosA78_g2 = cos( FUNC_Angle73_g2 );
				float _WindDirection164_g2 = _WindDirection;
				float2 localDirectionalEquation164_g2 = DirectionalEquation( _WindDirection164_g2 );
				float2 break165_g2 = localDirectionalEquation164_g2;
				float VAR_xLerp83_g2 = break165_g2.x;
				float lerpResult118_g2 = lerp( break109_g2.x , ( ( break109_g2.y * VAR_SinA80_g2 ) + ( break109_g2.x * VAR_CosA78_g2 ) ) , VAR_xLerp83_g2);
				float3 break98_g2 = VAR_VertexPosition21_g2;
				float3 break105_g2 = VAR_VertexPosition21_g2;
				float VAR_zLerp95_g2 = break165_g2.y;
				float lerpResult120_g2 = lerp( break105_g2.z , ( ( break105_g2.y * VAR_SinA80_g2 ) + ( break105_g2.z * VAR_CosA78_g2 ) ) , VAR_zLerp95_g2);
				float3 appendResult122_g2 = (float3(lerpResult118_g2 , ( ( break98_g2.y * VAR_CosA78_g2 ) - ( break98_g2.z * VAR_SinA80_g2 ) ) , lerpResult120_g2));
				float3 FUNC_vertexPos123_g2 = appendResult122_g2;
				float3 break221_g2 = FUNC_vertexPos123_g2;
				half FUNC_SinFunction195_g2 = sin( ( ( VAR_RandomTime16_g2 * 200.0 * ( 0.2 + v.ase_color.g ) ) + ( v.ase_color.g * 10.0 ) + FUNC_Turbulence36_g2 + ( VAR_VertexPosition21_g2.z / 2.0 ) ) );
				float temp_output_202_0_g2 = ( FUNC_SinFunction195_g2 * v.ase_color.b * ( FUNC_Angle73_g2 + ( VAR_WindStrength43_g2 / 200.0 ) ) );
				float lerpResult203_g2 = lerp( 0.0 , temp_output_202_0_g2 , VAR_xLerp83_g2);
				float lerpResult196_g2 = lerp( 0.0 , temp_output_202_0_g2 , VAR_zLerp95_g2);
				float3 appendResult197_g2 = (float3(( break221_g2.x + lerpResult203_g2 ) , break221_g2.y , ( break221_g2.z + lerpResult196_g2 )));
				float3 OUT_Grass_Standalone245_g2 = appendResult197_g2;
				float3 temp_output_5_0_g2 = mul( GetWorldToObjectMatrix(), float4( OUT_Grass_Standalone245_g2 , 0.0 ) ).xyz;
				
				o.ase_color = v.ase_color;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
				float3 defaultVertexValue = v.vertex.xyz;
				#else
				float3 defaultVertexValue = float3(0, 0, 0);
				#endif
				float3 vertexValue = temp_output_5_0_g2;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
				v.vertex.xyz = vertexValue;
				#else
				v.vertex.xyz += vertexValue;
				#endif
				v.ase_normal =  v.ase_normal ;

                VertexPositionInputs vertexInput = GetVertexPositionInputs(v.vertex.xyz);

                float3 lwWNormal = TransformObjectToWorldNormal(v.ase_normal);
				float3 lwWTangent = TransformObjectToWorldDir(v.ase_tangent.xyz);
				float3 lwWBinormal = normalize(cross(lwWNormal, lwWTangent) * v.ase_tangent.w);
				o.tSpace0 = float4(lwWTangent.x, lwWBinormal.x, lwWNormal.x, vertexInput.positionWS.x);
				o.tSpace1 = float4(lwWTangent.y, lwWBinormal.y, lwWNormal.y, vertexInput.positionWS.y);
				o.tSpace2 = float4(lwWTangent.z, lwWBinormal.z, lwWNormal.z, vertexInput.positionWS.z);
                
        	    OUTPUT_LIGHTMAP_UV(v.texcoord1, unity_LightmapST, o.lightmapUVOrVertexSH.xy);
        	    OUTPUT_SH(lwWNormal, o.lightmapUVOrVertexSH.xyz);

        	    half3 vertexLight = VertexLighting(vertexInput.positionWS, lwWNormal);
				#ifdef ASE_FOG
        			half fogFactor = ComputeFogFactor(vertexInput.positionCS.z);
				#else
					half fogFactor = 0;
				#endif
        	    o.fogFactorAndVertexLight = half4(fogFactor, vertexLight);
        	    o.clipPos = vertexInput.positionCS;

        		#ifdef _MAIN_LIGHT_SHADOWS
        			o.shadowCoord = GetShadowCoord(vertexInput);
        		#endif
        		return o;
        	}

        	half4 frag (GraphVertexOutput IN  ) : SV_Target
            {
            	UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(IN);

        		float3 WorldSpaceNormal = normalize(float3(IN.tSpace0.z,IN.tSpace1.z,IN.tSpace2.z));
				float3 WorldSpaceTangent = float3(IN.tSpace0.x,IN.tSpace1.x,IN.tSpace2.x);
				float3 WorldSpaceBiTangent = float3(IN.tSpace0.y,IN.tSpace1.y,IN.tSpace2.y);
				float3 WorldSpacePosition = float3(IN.tSpace0.w,IN.tSpace1.w,IN.tSpace2.w);
				float3 WorldSpaceViewDirection = SafeNormalize( _WorldSpaceCameraPos.xyz  - WorldSpacePosition );
    
				
				
		        float3 Albedo = IN.ase_color.rgb;
				float3 Normal = float3(0, 0, 1);
				float3 Emission = 0;
				float3 Specular = float3(0.5, 0.5, 0.5);
				float Metallic = 0;
				float Smoothness = 0.5;
				float Occlusion = 1;
				float Alpha = 1;
				float AlphaClipThreshold = 0;

        		InputData inputData;
        		inputData.positionWS = WorldSpacePosition;

        #ifdef _NORMALMAP
        	    inputData.normalWS = normalize(TransformTangentToWorld(Normal, half3x3(WorldSpaceTangent, WorldSpaceBiTangent, WorldSpaceNormal)));
        #else
            #if !SHADER_HINT_NICE_QUALITY
                inputData.normalWS = WorldSpaceNormal;
            #else
        	    inputData.normalWS = normalize(WorldSpaceNormal);
            #endif
        #endif

			#if !SHADER_HINT_NICE_QUALITY
        	    // viewDirection should be normalized here, but we avoid doing it as it's close enough and we save some ALU.
        	    inputData.viewDirectionWS = WorldSpaceViewDirection;
			#else
        	    inputData.viewDirectionWS = normalize(WorldSpaceViewDirection);
			#endif

        	    inputData.shadowCoord = IN.shadowCoord;
			#ifdef ASE_FOG
        	    inputData.fogCoord = IN.fogFactorAndVertexLight.x;
			#endif
        	    inputData.vertexLighting = IN.fogFactorAndVertexLight.yzw;
        	    inputData.bakedGI = SAMPLE_GI(IN.lightmapUVOrVertexSH.xy, IN.lightmapUVOrVertexSH.xyz, inputData.normalWS);

        		half4 color = LightweightFragmentPBR(
        			inputData, 
        			Albedo, 
        			Metallic, 
        			Specular, 
        			Smoothness, 
        			Occlusion, 
        			Emission, 
        			Alpha);

		#ifdef ASE_FOG
			#ifdef TERRAIN_SPLAT_ADDPASS
				color.rgb = MixFogColor(color.rgb, half3( 0, 0, 0 ), IN.fogFactorAndVertexLight.x );
			#else
				color.rgb = MixFog(color.rgb, IN.fogFactorAndVertexLight.x);
			#endif
		#endif

        #ifdef _ALPHATEST_ON
        		clip(Alpha - AlphaClipThreshold);
        #endif
		
		#ifdef LOD_FADE_CROSSFADE
				LODDitheringTransition (IN.clipPos.xyz, unity_LODFade.x);
		#endif
        		return color;
            }

        	ENDHLSL
        }

		
        Pass
        {
			
        	Name "ShadowCaster"
            Tags { "LightMode"="ShadowCaster" }

			ZWrite On
			ZTest LEqual

            HLSLPROGRAM
            #pragma multi_compile _ LOD_FADE_CROSSFADE
            #pragma multi_compile_fog
            #define ASE_FOG 1
            #define ASE_ABSOLUTE_VERTEX_POS 1
            #define ASE_SRP_VERSION 60902

            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x
            

            #pragma multi_compile_instancing

            #pragma vertex ShadowPassVertex
            #pragma fragment ShadowPassFragment


            #include "Packages/com.unity.render-pipelines.lightweight/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.lightweight/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.lightweight/ShaderLibrary/ShaderGraphFunctions.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"

            #define ASE_NEEDS_VERT_POSITION


			float _WindStrength;
			float _RandomWindOffset;
			float _WindPulse;
			float _WindDirection;
			CBUFFER_START( UnityPerMaterial )
			int _CullMode;
			float _GlobalWindInfluence;
			CBUFFER_END


            struct GraphVertexInput
            {
                float4 vertex : POSITION;
                float3 ase_normal : NORMAL;
				float4 ase_color : COLOR;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

        	struct VertexOutput
        	{
        	    float4 clipPos      : SV_POSITION;
				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				float3 worldPos : TEXCOORD0;
				#endif
				#if defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
				float4 shadowCoord : TEXCOORD1;
				#endif
				
                UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
        	};

			float2 DirectionalEquation( float _WindDirection )
			{
				float d = _WindDirection * 0.0174532924;
				float xL = cos(d) + 1 / 2;
				float zL = sin(d) + 1 / 2;
				return float2(zL,xL);
			}
			

            float3 _LightDirection;

            VertexOutput ShadowPassVertex(GraphVertexInput v )
        	{
        	    VertexOutput o;
        	    UNITY_SETUP_INSTANCE_ID(v);
                UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO (o);

				float3 VAR_VertexPosition21_g2 = mul( GetObjectToWorldMatrix(), float4( v.vertex.xyz , 0.0 ) ).xyz;
				float3 break109_g2 = VAR_VertexPosition21_g2;
				float VAR_WindStrength43_g2 = ( _WindStrength * _GlobalWindInfluence );
				float4 transform37_g2 = mul(GetObjectToWorldMatrix(),float4( 0,0,0,1 ));
				float2 appendResult38_g2 = (float2(transform37_g2.x , transform37_g2.z));
				float dotResult2_g3 = dot( appendResult38_g2 , float2( 12.9898,78.233 ) );
				float lerpResult8_g3 = lerp( 0.8 , ( ( _RandomWindOffset / 2.0 ) + 0.9 ) , frac( ( sin( dotResult2_g3 ) * 43758.55 ) ));
				float VAR_RandomTime16_g2 = ( ( _TimeParameters.x * 0.05 ) * lerpResult8_g3 );
				float FUNC_Turbulence36_g2 = ( sin( ( ( VAR_RandomTime16_g2 * 40.0 ) - ( VAR_VertexPosition21_g2.z / 15.0 ) ) ) * 0.5 );
				float VAR_WindPulse274_g2 = _WindPulse;
				float FUNC_Angle73_g2 = ( VAR_WindStrength43_g2 * ( 1.0 + sin( ( ( ( ( VAR_RandomTime16_g2 * 2.0 ) + FUNC_Turbulence36_g2 ) - ( VAR_VertexPosition21_g2.z / 50.0 ) ) - ( v.ase_color.r / 20.0 ) ) ) ) * sqrt( v.ase_color.r ) * 0.2 * VAR_WindPulse274_g2 );
				float VAR_SinA80_g2 = sin( FUNC_Angle73_g2 );
				float VAR_CosA78_g2 = cos( FUNC_Angle73_g2 );
				float _WindDirection164_g2 = _WindDirection;
				float2 localDirectionalEquation164_g2 = DirectionalEquation( _WindDirection164_g2 );
				float2 break165_g2 = localDirectionalEquation164_g2;
				float VAR_xLerp83_g2 = break165_g2.x;
				float lerpResult118_g2 = lerp( break109_g2.x , ( ( break109_g2.y * VAR_SinA80_g2 ) + ( break109_g2.x * VAR_CosA78_g2 ) ) , VAR_xLerp83_g2);
				float3 break98_g2 = VAR_VertexPosition21_g2;
				float3 break105_g2 = VAR_VertexPosition21_g2;
				float VAR_zLerp95_g2 = break165_g2.y;
				float lerpResult120_g2 = lerp( break105_g2.z , ( ( break105_g2.y * VAR_SinA80_g2 ) + ( break105_g2.z * VAR_CosA78_g2 ) ) , VAR_zLerp95_g2);
				float3 appendResult122_g2 = (float3(lerpResult118_g2 , ( ( break98_g2.y * VAR_CosA78_g2 ) - ( break98_g2.z * VAR_SinA80_g2 ) ) , lerpResult120_g2));
				float3 FUNC_vertexPos123_g2 = appendResult122_g2;
				float3 break221_g2 = FUNC_vertexPos123_g2;
				half FUNC_SinFunction195_g2 = sin( ( ( VAR_RandomTime16_g2 * 200.0 * ( 0.2 + v.ase_color.g ) ) + ( v.ase_color.g * 10.0 ) + FUNC_Turbulence36_g2 + ( VAR_VertexPosition21_g2.z / 2.0 ) ) );
				float temp_output_202_0_g2 = ( FUNC_SinFunction195_g2 * v.ase_color.b * ( FUNC_Angle73_g2 + ( VAR_WindStrength43_g2 / 200.0 ) ) );
				float lerpResult203_g2 = lerp( 0.0 , temp_output_202_0_g2 , VAR_xLerp83_g2);
				float lerpResult196_g2 = lerp( 0.0 , temp_output_202_0_g2 , VAR_zLerp95_g2);
				float3 appendResult197_g2 = (float3(( break221_g2.x + lerpResult203_g2 ) , break221_g2.y , ( break221_g2.z + lerpResult196_g2 )));
				float3 OUT_Grass_Standalone245_g2 = appendResult197_g2;
				float3 temp_output_5_0_g2 = mul( GetWorldToObjectMatrix(), float4( OUT_Grass_Standalone245_g2 , 0.0 ) ).xyz;
				
				#ifdef ASE_ABSOLUTE_VERTEX_POS
				float3 defaultVertexValue = v.vertex.xyz;
				#else
				float3 defaultVertexValue = float3(0, 0, 0);
				#endif
				float3 vertexValue = temp_output_5_0_g2;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
				v.vertex.xyz = vertexValue;
				#else
				v.vertex.xyz += vertexValue;
				#endif

				v.ase_normal =  v.ase_normal ;

        	    float3 positionWS = TransformObjectToWorld(v.vertex.xyz);
                float3 normalWS = TransformObjectToWorldDir(v.ase_normal);

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				o.worldPos = positionWS;
				#endif

                float invNdotL = 1.0 - saturate(dot(_LightDirection, normalWS));
                float scale = invNdotL * _ShadowBias.y;

                positionWS = _LightDirection * _ShadowBias.xxx + positionWS;
				positionWS = normalWS * scale.xxx + positionWS;
                float4 clipPos = TransformWorldToHClip(positionWS);

        		#if UNITY_REVERSED_Z
        			clipPos.z = min(clipPos.z, clipPos.w * UNITY_NEAR_CLIP_VALUE);
        		#else
        			clipPos.z = max(clipPos.z, clipPos.w * UNITY_NEAR_CLIP_VALUE);
        		#endif

				#if defined(_MAIN_LIGHT_SHADOWS) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					VertexPositionInputs vertexInput = (VertexPositionInputs)0;
					vertexInput.positionWS = positionWS;
					vertexInput.positionCS = clipPos;
        			o.shadowCoord = GetShadowCoord(vertexInput);
        		#endif

                o.clipPos = clipPos;
        	    return o;
        	}

            half4 ShadowPassFragment(VertexOutput IN  ) : SV_TARGET
            {
                UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(IN);

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				float3 WorldPosition = IN.worldPos;
				#endif
				#if defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
				float4 ShadowCoords = IN.shadowCoord;
				#endif

				

				float Alpha = 1;
				float AlphaClipThreshold = AlphaClipThreshold;

				#ifdef _ALPHATEST_ON
					clip(Alpha - AlphaClipThreshold);
				#endif

				#ifdef LOD_FADE_CROSSFADE
					LODDitheringTransition (IN.clipPos.xyz, unity_LODFade.x);
				#endif
				return 0;
            }

            ENDHLSL
        }

		
        Pass
        {
			
        	Name "DepthOnly"
            Tags { "LightMode"="DepthOnly" }

            ZWrite On
			ColorMask 0

            HLSLPROGRAM
            #pragma multi_compile _ LOD_FADE_CROSSFADE
            #pragma multi_compile_fog
            #define ASE_FOG 1
            #define ASE_ABSOLUTE_VERTEX_POS 1
            #define ASE_SRP_VERSION 60902

            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x

            #pragma multi_compile_instancing

            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.lightweight/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.lightweight/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.lightweight/ShaderLibrary/ShaderGraphFunctions.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"

            #define ASE_NEEDS_VERT_POSITION


			float _WindStrength;
			float _RandomWindOffset;
			float _WindPulse;
			float _WindDirection;
			CBUFFER_START( UnityPerMaterial )
			int _CullMode;
			float _GlobalWindInfluence;
			CBUFFER_END


            struct GraphVertexInput
            {
                float4 vertex : POSITION;
				float3 ase_normal : NORMAL;
				float4 ase_color : COLOR;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

        	struct VertexOutput
        	{
        	    float4 clipPos      : SV_POSITION;
				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				float3 worldPos : TEXCOORD0;
				#endif
				#if defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
				float4 shadowCoord : TEXCOORD1;
				#endif
				
                UNITY_VERTEX_INPUT_INSTANCE_ID
                UNITY_VERTEX_OUTPUT_STEREO
        	};

			float2 DirectionalEquation( float _WindDirection )
			{
				float d = _WindDirection * 0.0174532924;
				float xL = cos(d) + 1 / 2;
				float zL = sin(d) + 1 / 2;
				return float2(zL,xL);
			}
			

            VertexOutput vert(GraphVertexInput v  )
            {
                VertexOutput o = (VertexOutput)0;
        	    UNITY_SETUP_INSTANCE_ID(v);
                UNITY_TRANSFER_INSTANCE_ID(v, o);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				float3 VAR_VertexPosition21_g2 = mul( GetObjectToWorldMatrix(), float4( v.vertex.xyz , 0.0 ) ).xyz;
				float3 break109_g2 = VAR_VertexPosition21_g2;
				float VAR_WindStrength43_g2 = ( _WindStrength * _GlobalWindInfluence );
				float4 transform37_g2 = mul(GetObjectToWorldMatrix(),float4( 0,0,0,1 ));
				float2 appendResult38_g2 = (float2(transform37_g2.x , transform37_g2.z));
				float dotResult2_g3 = dot( appendResult38_g2 , float2( 12.9898,78.233 ) );
				float lerpResult8_g3 = lerp( 0.8 , ( ( _RandomWindOffset / 2.0 ) + 0.9 ) , frac( ( sin( dotResult2_g3 ) * 43758.55 ) ));
				float VAR_RandomTime16_g2 = ( ( _TimeParameters.x * 0.05 ) * lerpResult8_g3 );
				float FUNC_Turbulence36_g2 = ( sin( ( ( VAR_RandomTime16_g2 * 40.0 ) - ( VAR_VertexPosition21_g2.z / 15.0 ) ) ) * 0.5 );
				float VAR_WindPulse274_g2 = _WindPulse;
				float FUNC_Angle73_g2 = ( VAR_WindStrength43_g2 * ( 1.0 + sin( ( ( ( ( VAR_RandomTime16_g2 * 2.0 ) + FUNC_Turbulence36_g2 ) - ( VAR_VertexPosition21_g2.z / 50.0 ) ) - ( v.ase_color.r / 20.0 ) ) ) ) * sqrt( v.ase_color.r ) * 0.2 * VAR_WindPulse274_g2 );
				float VAR_SinA80_g2 = sin( FUNC_Angle73_g2 );
				float VAR_CosA78_g2 = cos( FUNC_Angle73_g2 );
				float _WindDirection164_g2 = _WindDirection;
				float2 localDirectionalEquation164_g2 = DirectionalEquation( _WindDirection164_g2 );
				float2 break165_g2 = localDirectionalEquation164_g2;
				float VAR_xLerp83_g2 = break165_g2.x;
				float lerpResult118_g2 = lerp( break109_g2.x , ( ( break109_g2.y * VAR_SinA80_g2 ) + ( break109_g2.x * VAR_CosA78_g2 ) ) , VAR_xLerp83_g2);
				float3 break98_g2 = VAR_VertexPosition21_g2;
				float3 break105_g2 = VAR_VertexPosition21_g2;
				float VAR_zLerp95_g2 = break165_g2.y;
				float lerpResult120_g2 = lerp( break105_g2.z , ( ( break105_g2.y * VAR_SinA80_g2 ) + ( break105_g2.z * VAR_CosA78_g2 ) ) , VAR_zLerp95_g2);
				float3 appendResult122_g2 = (float3(lerpResult118_g2 , ( ( break98_g2.y * VAR_CosA78_g2 ) - ( break98_g2.z * VAR_SinA80_g2 ) ) , lerpResult120_g2));
				float3 FUNC_vertexPos123_g2 = appendResult122_g2;
				float3 break221_g2 = FUNC_vertexPos123_g2;
				half FUNC_SinFunction195_g2 = sin( ( ( VAR_RandomTime16_g2 * 200.0 * ( 0.2 + v.ase_color.g ) ) + ( v.ase_color.g * 10.0 ) + FUNC_Turbulence36_g2 + ( VAR_VertexPosition21_g2.z / 2.0 ) ) );
				float temp_output_202_0_g2 = ( FUNC_SinFunction195_g2 * v.ase_color.b * ( FUNC_Angle73_g2 + ( VAR_WindStrength43_g2 / 200.0 ) ) );
				float lerpResult203_g2 = lerp( 0.0 , temp_output_202_0_g2 , VAR_xLerp83_g2);
				float lerpResult196_g2 = lerp( 0.0 , temp_output_202_0_g2 , VAR_zLerp95_g2);
				float3 appendResult197_g2 = (float3(( break221_g2.x + lerpResult203_g2 ) , break221_g2.y , ( break221_g2.z + lerpResult196_g2 )));
				float3 OUT_Grass_Standalone245_g2 = appendResult197_g2;
				float3 temp_output_5_0_g2 = mul( GetWorldToObjectMatrix(), float4( OUT_Grass_Standalone245_g2 , 0.0 ) ).xyz;
				
				#ifdef ASE_ABSOLUTE_VERTEX_POS
				float3 defaultVertexValue = v.vertex.xyz;
				#else
				float3 defaultVertexValue = float3(0, 0, 0);
				#endif
				float3 vertexValue = temp_output_5_0_g2;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
				v.vertex.xyz = vertexValue;
				#else
				v.vertex.xyz += vertexValue;
				#endif

				v.ase_normal =  v.ase_normal ;

				float3 positionWS = TransformObjectToWorld( v.vertex.xyz );
				float4 clipPos = TransformWorldToHClip( positionWS );

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				o.worldPos = positionWS;
				#endif

				#if defined(_MAIN_LIGHT_SHADOWS) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					VertexPositionInputs vertexInput = (VertexPositionInputs)0;
					vertexInput.positionWS = positionWS;
					vertexInput.positionCS = clipPos;
        			o.shadowCoord = GetShadowCoord(vertexInput);
        		#endif

        	    o.clipPos = clipPos;
        	    return o;
            }

            half4 frag(VertexOutput IN  ) : SV_TARGET
            {
                UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( IN );

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				float3 WorldPosition = IN.worldPos;
				#endif
				#if defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
				float4 ShadowCoords = IN.shadowCoord;
				#endif

				

				float Alpha = 1;
				float AlphaClipThreshold = AlphaClipThreshold;

				#ifdef _ALPHATEST_ON
					clip(Alpha - AlphaClipThreshold);
				#endif
				#ifdef LOD_FADE_CROSSFADE
					LODDitheringTransition (IN.clipPos.xyz, unity_LODFade.x);
				#endif
				return 0;
            }
            ENDHLSL
        }

		
        Pass
        {
			
        	Name "Meta"
            Tags { "LightMode"="Meta" }

            Cull Off

            HLSLPROGRAM
            #pragma multi_compile _ LOD_FADE_CROSSFADE
            #pragma multi_compile_fog
            #define ASE_FOG 1
            #define ASE_ABSOLUTE_VERTEX_POS 1
            #define ASE_SRP_VERSION 60902

            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x

            #pragma vertex vert
            #pragma fragment frag

			
            #include "Packages/com.unity.render-pipelines.lightweight/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.lightweight/ShaderLibrary/MetaInput.hlsl"
            #include "Packages/com.unity.render-pipelines.lightweight/ShaderLibrary/ShaderGraphFunctions.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"

            #define ASE_NEEDS_VERT_POSITION


			float _WindStrength;
			float _RandomWindOffset;
			float _WindPulse;
			float _WindDirection;
			CBUFFER_START( UnityPerMaterial )
			int _CullMode;
			float _GlobalWindInfluence;
			CBUFFER_END


            struct GraphVertexInput
            {
                float4 vertex : POSITION;
				float3 ase_normal : NORMAL;
				float4 texcoord1 : TEXCOORD1;
				float4 texcoord2 : TEXCOORD2;
				float4 ase_color : COLOR;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

        	struct VertexOutput
        	{
        	    float4 clipPos      : SV_POSITION;
				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				float3 worldPos : TEXCOORD0;
				#endif
				#if defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
				float4 shadowCoord : TEXCOORD1;
				#endif
				float4 ase_color : COLOR;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                UNITY_VERTEX_OUTPUT_STEREO
        	};

			float2 DirectionalEquation( float _WindDirection )
			{
				float d = _WindDirection * 0.0174532924;
				float xL = cos(d) + 1 / 2;
				float zL = sin(d) + 1 / 2;
				return float2(zL,xL);
			}
			

            VertexOutput vert(GraphVertexInput v  )
            {
                VertexOutput o = (VertexOutput)0;
        	    UNITY_SETUP_INSTANCE_ID(v);
                UNITY_TRANSFER_INSTANCE_ID(v, o);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				float3 VAR_VertexPosition21_g2 = mul( GetObjectToWorldMatrix(), float4( v.vertex.xyz , 0.0 ) ).xyz;
				float3 break109_g2 = VAR_VertexPosition21_g2;
				float VAR_WindStrength43_g2 = ( _WindStrength * _GlobalWindInfluence );
				float4 transform37_g2 = mul(GetObjectToWorldMatrix(),float4( 0,0,0,1 ));
				float2 appendResult38_g2 = (float2(transform37_g2.x , transform37_g2.z));
				float dotResult2_g3 = dot( appendResult38_g2 , float2( 12.9898,78.233 ) );
				float lerpResult8_g3 = lerp( 0.8 , ( ( _RandomWindOffset / 2.0 ) + 0.9 ) , frac( ( sin( dotResult2_g3 ) * 43758.55 ) ));
				float VAR_RandomTime16_g2 = ( ( _TimeParameters.x * 0.05 ) * lerpResult8_g3 );
				float FUNC_Turbulence36_g2 = ( sin( ( ( VAR_RandomTime16_g2 * 40.0 ) - ( VAR_VertexPosition21_g2.z / 15.0 ) ) ) * 0.5 );
				float VAR_WindPulse274_g2 = _WindPulse;
				float FUNC_Angle73_g2 = ( VAR_WindStrength43_g2 * ( 1.0 + sin( ( ( ( ( VAR_RandomTime16_g2 * 2.0 ) + FUNC_Turbulence36_g2 ) - ( VAR_VertexPosition21_g2.z / 50.0 ) ) - ( v.ase_color.r / 20.0 ) ) ) ) * sqrt( v.ase_color.r ) * 0.2 * VAR_WindPulse274_g2 );
				float VAR_SinA80_g2 = sin( FUNC_Angle73_g2 );
				float VAR_CosA78_g2 = cos( FUNC_Angle73_g2 );
				float _WindDirection164_g2 = _WindDirection;
				float2 localDirectionalEquation164_g2 = DirectionalEquation( _WindDirection164_g2 );
				float2 break165_g2 = localDirectionalEquation164_g2;
				float VAR_xLerp83_g2 = break165_g2.x;
				float lerpResult118_g2 = lerp( break109_g2.x , ( ( break109_g2.y * VAR_SinA80_g2 ) + ( break109_g2.x * VAR_CosA78_g2 ) ) , VAR_xLerp83_g2);
				float3 break98_g2 = VAR_VertexPosition21_g2;
				float3 break105_g2 = VAR_VertexPosition21_g2;
				float VAR_zLerp95_g2 = break165_g2.y;
				float lerpResult120_g2 = lerp( break105_g2.z , ( ( break105_g2.y * VAR_SinA80_g2 ) + ( break105_g2.z * VAR_CosA78_g2 ) ) , VAR_zLerp95_g2);
				float3 appendResult122_g2 = (float3(lerpResult118_g2 , ( ( break98_g2.y * VAR_CosA78_g2 ) - ( break98_g2.z * VAR_SinA80_g2 ) ) , lerpResult120_g2));
				float3 FUNC_vertexPos123_g2 = appendResult122_g2;
				float3 break221_g2 = FUNC_vertexPos123_g2;
				half FUNC_SinFunction195_g2 = sin( ( ( VAR_RandomTime16_g2 * 200.0 * ( 0.2 + v.ase_color.g ) ) + ( v.ase_color.g * 10.0 ) + FUNC_Turbulence36_g2 + ( VAR_VertexPosition21_g2.z / 2.0 ) ) );
				float temp_output_202_0_g2 = ( FUNC_SinFunction195_g2 * v.ase_color.b * ( FUNC_Angle73_g2 + ( VAR_WindStrength43_g2 / 200.0 ) ) );
				float lerpResult203_g2 = lerp( 0.0 , temp_output_202_0_g2 , VAR_xLerp83_g2);
				float lerpResult196_g2 = lerp( 0.0 , temp_output_202_0_g2 , VAR_zLerp95_g2);
				float3 appendResult197_g2 = (float3(( break221_g2.x + lerpResult203_g2 ) , break221_g2.y , ( break221_g2.z + lerpResult196_g2 )));
				float3 OUT_Grass_Standalone245_g2 = appendResult197_g2;
				float3 temp_output_5_0_g2 = mul( GetWorldToObjectMatrix(), float4( OUT_Grass_Standalone245_g2 , 0.0 ) ).xyz;
				
				o.ase_color = v.ase_color;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
				float3 defaultVertexValue = v.vertex.xyz;
				#else
				float3 defaultVertexValue = float3(0, 0, 0);
				#endif
				float3 vertexValue = temp_output_5_0_g2;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
				v.vertex.xyz = vertexValue;
				#else
				v.vertex.xyz += vertexValue;
				#endif

				v.ase_normal =  v.ase_normal ;

				float3 positionWS = TransformObjectToWorld( v.vertex.xyz );

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				o.worldPos = positionWS;
				#endif

				#if !defined( ASE_SRP_VERSION ) || ASE_SRP_VERSION  > 51300				
					o.clipPos = MetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord1.xy, unity_LightmapST, unity_DynamicLightmapST);
				#else
					o.clipPos = MetaVertexPosition (v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST);
				#endif

				#if defined(_MAIN_LIGHT_SHADOWS) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					VertexPositionInputs vertexInput = (VertexPositionInputs)0;
					vertexInput.positionWS = positionWS;
					vertexInput.positionCS = o.clipPos;
        			o.shadowCoord = GetShadowCoord(vertexInput);
        		#endif
        	    return o;
            }

            half4 frag(VertexOutput IN  ) : SV_TARGET
            {
                UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( IN );

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				float3 WorldPosition = IN.worldPos;
				#endif
				#if defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
				float4 ShadowCoords = IN.shadowCoord;
				#endif

           		
				
		        float3 Albedo = IN.ase_color.rgb;
				float3 Emission = 0;
				float Alpha = 1;
				float AlphaClipThreshold = 0;

				#ifdef _ALPHATEST_ON
					clip(Alpha - AlphaClipThreshold);
				#endif

                MetaInput metaInput = (MetaInput)0;
                metaInput.Albedo = Albedo;
                metaInput.Emission = Emission;
                
                return MetaFragment(metaInput);
            }
            ENDHLSL
        }
		
    }
    Fallback "Hidden/InternalErrorShader"
	CustomEditor "ASEMaterialInspector"
	
}
/*ASEBEGIN
Version=17800
478;164;1008;682;1810.71;390.456;1.464041;True;False
Node;AmplifyShaderEditor.CommentaryNode;22;-976.9675,61.41877;Inherit;False;275.1868;247.6812;;1;19;Vertex Position;0,1,0.09019608,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;24;-1409.197,-326.6083;Inherit;False;674.4768;271.9942;;1;1;Albedo;1,0.1254902,0.1254902,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;23;-1397.893,68.09306;Inherit;False;275;165;;1;17;Variables;1,0,0.7254902,1;0;0
Node;AmplifyShaderEditor.IntNode;17;-1372.171,118.5078;Inherit;False;Property;_CullMode;Cull Mode;9;1;[Enum];Create;True;3;Off;0;Front;1;Back;2;0;True;0;2;0;0;1;INT;0
Node;AmplifyShaderEditor.VertexColorNode;1;-1338.694,-258.3241;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;19;-925.9949,163.2096;Inherit;False;Mtree Wind;0;;2;d710ffc7589a70c42a3e6c5220c6279d;7,282,0,280,0,278,0,255,4,269,1,281,0,272,0;0;1;FLOAT3;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;26;-284.9035,-258.0691;Float;False;True;-1;2;ASEMaterialInspector;0;2;Hidden/Mtree/SRP/VertexColorShader LWRP;1976390536c6c564abb90fe41f6ee334;True;Base;0;1;Base;11;False;False;False;True;0;True;17;False;False;False;False;False;True;3;RenderPipeline=LightweightPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;2;0;True;1;1;False;-1;0;False;-1;1;1;False;-1;0;False;-1;False;False;False;True;True;True;True;True;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;1;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;1;LightMode=LightweightForward;False;0;Hidden/InternalErrorShader;0;0;Standard;11;Workflow;1;Surface;0;  Blend;0;Two Sided;1;Cast Shadows;1;Receive Shadows;1;LOD CrossFade;1;Built-in Fog;1;Meta Pass;1;Extra Pre Pass;0;Vertex Position,InvertActionOnDeselection;0;0;5;False;True;True;True;True;False;;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;27;-284.9035,-258.0691;Float;False;False;-1;2;ASEMaterialInspector;0;1;New Amplify Shader;1976390536c6c564abb90fe41f6ee334;True;ShadowCaster;0;2;ShadowCaster;0;False;False;False;True;0;False;-1;False;False;False;False;False;True;3;RenderPipeline=LightweightPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;2;0;False;False;False;False;False;False;True;1;False;-1;True;3;False;-1;False;True;1;LightMode=ShadowCaster;False;0;Hidden/InternalErrorShader;0;0;Standard;0;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;28;-284.9035,-258.0691;Float;False;False;-1;2;ASEMaterialInspector;0;1;New Amplify Shader;1976390536c6c564abb90fe41f6ee334;True;DepthOnly;0;3;DepthOnly;0;False;False;False;True;0;False;-1;False;False;False;False;False;True;3;RenderPipeline=LightweightPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;2;0;False;False;False;False;True;False;False;False;False;0;False;-1;False;True;1;False;-1;False;False;True;1;LightMode=DepthOnly;False;0;Hidden/InternalErrorShader;0;0;Standard;0;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;29;-284.9035,-258.0691;Float;False;False;-1;2;ASEMaterialInspector;0;1;New Amplify Shader;1976390536c6c564abb90fe41f6ee334;True;Meta;0;4;Meta;0;False;False;False;True;0;False;-1;False;False;False;False;False;True;3;RenderPipeline=LightweightPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;2;0;False;False;False;True;2;False;-1;False;False;False;False;False;True;1;LightMode=Meta;False;0;Hidden/InternalErrorShader;0;0;Standard;0;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;30;-284.9035,-258.0691;Float;False;False;-1;2;ASEMaterialInspector;0;1;New Amplify Shader;1976390536c6c564abb90fe41f6ee334;True;ExtraPrePass;0;0;ExtraPrePass;5;False;False;False;True;0;False;-1;False;False;False;False;False;True;3;RenderPipeline=LightweightPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;2;0;True;1;1;False;-1;0;False;-1;0;1;False;-1;0;False;-1;False;False;True;0;False;-1;True;True;True;True;True;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;1;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;0;False;0;Hidden/InternalErrorShader;0;0;Standard;0;0
Node;AmplifyShaderEditor.CommentaryNode;25;-334.9035,-308.0691;Inherit;False;313;505;;0;Output;0,0,0,1;0;0
WireConnection;26;0;1;0
WireConnection;26;8;19;0
ASEEND*/
//CHKSM=4E6AE7DEB5C48DCD4D93F92A787ACB2F16087277