Shader "Custom/CloudSkybox" {
   Properties {
      _Cube ("Environment Map", Cube) = "white" {}
      _SkyColor ("Sky Tint", Color) = (1,1,1,1)
      _GroundColor ("Ground Tint", Color) = (0.3,1,0.3,1)
      _BlendGround("Blend Ground/Sky", Range(1,100)) = 1
      _GroundPosition("Ground Position", Range(0,1)) = 0.5
   }

   SubShader {
      Tags { "Queue"="Background"  }

      Pass {
         ZWrite Off 
         Cull Off 

         CGPROGRAM
         #pragma vertex vert
         #pragma fragment frag

         // User-specified uniforms
         samplerCUBE _Cube;

         struct vertexInput {
            float4 vertex : POSITION;
            float3 texcoord : TEXCOORD0;
         };

         struct vertexOutput {
            float4 vertex : SV_POSITION;
            float3 texcoord : TEXCOORD0;
         };

         vertexOutput vert(vertexInput input)
         {
            vertexOutput output;
            output.vertex = mul(UNITY_MATRIX_MVP, input.vertex);
            output.texcoord = input.texcoord;
            return output;
         }

         float4 _SkyColor;
         float4 _GroundColor;
         float _BlendGround;
         float _GroundPosition;

         fixed4 frag (vertexOutput input) : COLOR
         {
         	float yCoord = ((input.texcoord.y+1)/2);
         	float smoothCoord = 1/(1+exp(-(_BlendGround*20)*yCoord+_BlendGround*10*_GroundPosition*2));
         	float4 result = lerp(_GroundColor, _SkyColor, smoothCoord);
            return result;
         }
         ENDCG 
      }
   } 	
}