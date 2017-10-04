// Upgrade NOTE: commented out 'float4 unity_DynamicLightmapST', a built-in variable
// Upgrade NOTE: commented out 'float4 unity_LightmapST', a built-in variable

// Shader created with Shader Forge v1.04 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.04;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:1,culm:0,dpts:2,wrdp:True,dith:2,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:7927,x:33254,y:32721,varname:node_7927,prsc:2|emission-6341-OUT;n:type:ShaderForge.SFN_Tex2d,id:3353,x:32655,y:32712,ptovrint:False,ptlb:Diffuse,ptin:_Diffuse,varname:node_3353,prsc:2,tex:2e741e1330d4f9d48b18dd43502459e1,ntxv:0,isnm:False|UVIN-699-OUT;n:type:ShaderForge.SFN_ValueProperty,id:3160,x:32921,y:32468,ptovrint:False,ptlb:Intensity,ptin:_Intensity,varname:node_3160,prsc:2,glob:False,v1:2;n:type:ShaderForge.SFN_Tex2d,id:261,x:32247,y:32644,ptovrint:False,ptlb:Distortion,ptin:_Distortion,varname:node_261,prsc:2,tex:177d0d41c89ba1844b0e98b2f3877b38,ntxv:0,isnm:False|UVIN-2410-OUT;n:type:ShaderForge.SFN_Time,id:468,x:31894,y:32954,varname:node_468,prsc:2;n:type:ShaderForge.SFN_Fmod,id:5128,x:32245,y:32976,varname:node_5128,prsc:2|A-6977-OUT,B-3535-OUT;n:type:ShaderForge.SFN_Vector1,id:3535,x:32245,y:32922,varname:node_3535,prsc:2,v1:1;n:type:ShaderForge.SFN_Multiply,id:6977,x:32083,y:32975,varname:node_6977,prsc:2|A-468-T,B-3537-OUT;n:type:ShaderForge.SFN_ValueProperty,id:3537,x:32083,y:32922,ptovrint:False,ptlb:Speed,ptin:_Speed,varname:_node_3160_copy,prsc:2,glob:False,v1:2;n:type:ShaderForge.SFN_TexCoord,id:1422,x:32423,y:32375,varname:node_1422,prsc:2,uv:0;n:type:ShaderForge.SFN_Add,id:699,x:32637,y:32426,varname:node_699,prsc:2|A-1422-UVOUT,B-1670-OUT;n:type:ShaderForge.SFN_ValueProperty,id:4742,x:32247,y:32562,ptovrint:False,ptlb:DistortionScale,ptin:_DistortionScale,varname:node_4742,prsc:2,glob:False,v1:0.1;n:type:ShaderForge.SFN_Multiply,id:1670,x:32423,y:32528,varname:node_1670,prsc:2|A-261-R,B-4742-OUT;n:type:ShaderForge.SFN_Append,id:2410,x:32023,y:32602,varname:node_2410,prsc:2|A-2021-U,B-3809-OUT;n:type:ShaderForge.SFN_TexCoord,id:2021,x:31773,y:32581,varname:node_2021,prsc:2,uv:0;n:type:ShaderForge.SFN_Add,id:3809,x:31951,y:32726,varname:node_3809,prsc:2|A-2021-V,B-5128-OUT;n:type:ShaderForge.SFN_Multiply,id:6218,x:32895,y:32584,varname:node_6218,prsc:2|A-261-RGB,B-3353-RGB,C-3160-OUT;n:type:ShaderForge.SFN_Add,id:6341,x:33075,y:32692,varname:node_6341,prsc:2|A-6218-OUT,B-3353-RGB,C-5538-OUT;n:type:ShaderForge.SFN_Multiply,id:5538,x:32436,y:32712,varname:node_5538,prsc:2|A-261-R,B-5547-OUT;n:type:ShaderForge.SFN_Vector1,id:5547,x:32436,y:32840,varname:node_5547,prsc:2,v1:0.1;proporder:3353-3160-261-3537-4742;pass:END;sub:END;*/

Shader "Shader Forge/map_screen" {
    Properties {
        _Diffuse ("Diffuse", 2D) = "white" {}
        _Intensity ("Intensity", Float ) = 2
        _Distortion ("Distortion", 2D) = "white" {}
        _Speed ("Speed", Float ) = 2
        _DistortionScale ("DistortionScale", Float ) = 0.1
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            // float4 unity_LightmapST;
            #ifdef DYNAMICLIGHTMAP_ON
                // float4 unity_DynamicLightmapST;
            #endif
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            uniform float _Intensity;
            uniform sampler2D _Distortion; uniform float4 _Distortion_ST;
            uniform float _Speed;
            uniform float _DistortionScale;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                #ifndef LIGHTMAP_OFF
                    float4 uvLM : TEXCOORD2;
                #else
                    float3 shLight : TEXCOORD2;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
/////// Vectors:
////// Lighting:
////// Emissive:
                float4 node_468 = _Time + _TimeEditor;
                float2 node_2410 = float2(i.uv0.r,(i.uv0.g+fmod((node_468.g*_Speed),1.0)));
                float4 _Distortion_var = tex2D(_Distortion,TRANSFORM_TEX(node_2410, _Distortion));
                float2 node_699 = (i.uv0+(_Distortion_var.r*_DistortionScale));
                float4 _Diffuse_var = tex2D(_Diffuse,TRANSFORM_TEX(node_699, _Diffuse));
                float3 emissive = ((_Distortion_var.rgb*_Diffuse_var.rgb*_Intensity)+_Diffuse_var.rgb+(_Distortion_var.r*0.1));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
