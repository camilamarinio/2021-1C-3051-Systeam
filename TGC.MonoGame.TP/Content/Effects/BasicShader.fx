#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

// Custom Effects - https://docs.monogame.net/articles/content/custom_effects.html
// High-level shader language (HLSL) - https://docs.microsoft.com/en-us/windows/win32/direct3dhlsl/dx-graphics-hlsl
// Programming guide for HLSL - https://docs.microsoft.com/en-us/windows/win32/direct3dhlsl/dx-graphics-hlsl-pguide
// Reference for HLSL - https://docs.microsoft.com/en-us/windows/win32/direct3dhlsl/dx-graphics-hlsl-reference
// HLSL Semantics - https://docs.microsoft.com/en-us/windows/win32/direct3dhlsl/dx-graphics-hlsl-semantics

float4x4 World;
float4x4 View;
float4x4 Projection;

float3 DiffuseColor;

float Time = 0;

struct VertexShaderInput
{
	float4 Position : POSITION0;
    float2 TextureCoordinated : TEXCOORD0;
};

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
    float2 TextureCoordinated : TEXCOORD0;
};

texture ModelTexture;
sampler2D textureSample = sampler_state
{
    Texture = (ModelTexture);
    MagFilter = Linear;
    MinFilter = Linear;
    AdDressU = Clamp;
    AdDressV = Clamp;
};

VertexShaderOutput MainVS(in VertexShaderInput input)
{
    // Clear the output
	VertexShaderOutput output = (VertexShaderOutput)0;
    // Model space to World space
    float4 worldPosition = mul(input.Position, World);
    // World space to View space
    float4 viewPosition = mul(worldPosition, View);	
	// View space to Projection space
    output.Position = mul(viewPosition, Projection);

    output.TextureCoordinated = input.TextureCoordinated;
    
    return output;
}

float4 MainPS(VertexShaderOutput input) : COLOR
{
    //return tex2D(textureSample, input.TextureCoordinated * 0.5 + 0.5 * float2(sin(Time) * 0.5 + 0.5, 0.0));
    return tex2D(textureSample, input.TextureCoordinated);

}

technique BasicColorDrawing
{
	pass P0
	{
		VertexShader = compile VS_SHADERMODEL MainVS();
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};
