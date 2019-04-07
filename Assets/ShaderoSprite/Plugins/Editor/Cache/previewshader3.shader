//////////////////////////////////////////////////////////////
/// Shadero Sprite: Sprite Shader Editor - by VETASOFT 2018 //
/// Shader generate with Shadero 1.9.6                      //
/// http://u3d.as/V7t #AssetStore                           //
/// http://www.shadero.com #Docs                            //
//////////////////////////////////////////////////////////////

Shader "Shadero Previews/PreviewXATXQ3"
{
Properties
{
[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
_Hologram_Value_1("_Hologram_Value_1", Range(0, 1)) = 0
_Hologram_Speed_1("_Hologram_Speed_1", Range(0, 4)) = 2.213
_Outline_Size_1("_Outline_Size_1", Range(1, 16)) = 0.522
_Outline_Color_1("_Outline_Color_1", COLOR) = (0.5515446,1,0.1273585,1)
_Outline_HDR_1("_Outline_HDR_1", Range(0, 2)) = 1
_Sub_Fade_1("_Sub_Fade_1", Range(0, 1)) = 1
_Add_Fade_1("_Add_Fade_1", Range(0, 4)) = 0
_SpriteFade("SpriteFade", Range(0, 1)) = 1.0

// required for UI.Mask
[HideInInspector]_StencilComp("Stencil Comparison", Float) = 8
[HideInInspector]_Stencil("Stencil ID", Float) = 0
[HideInInspector]_StencilOp("Stencil Operation", Float) = 0
[HideInInspector]_StencilWriteMask("Stencil Write Mask", Float) = 255
[HideInInspector]_StencilReadMask("Stencil Read Mask", Float) = 255
[HideInInspector]_ColorMask("Color Mask", Float) = 15

}

SubShader
{

Tags {"Queue" = "Transparent" "IgnoreProjector" = "true" "RenderType" = "Transparent" "PreviewType"="Plane" "CanUseSpriteAtlas"="True" }
ZWrite Off Blend SrcAlpha OneMinusSrcAlpha Cull Off 

// required for UI.Mask
Stencil
{
Ref [_Stencil]
Comp [_StencilComp]
Pass [_StencilOp]
ReadMask [_StencilReadMask]
WriteMask [_StencilWriteMask]
}

Pass
{

CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma fragmentoption ARB_precision_hint_fastest
#include "UnityCG.cginc"

struct appdata_t{
float4 vertex   : POSITION;
float4 color    : COLOR;
float2 texcoord : TEXCOORD0;
};

struct v2f
{
float2 texcoord  : TEXCOORD0;
float4 vertex   : SV_POSITION;
float4 color    : COLOR;
};

sampler2D _MainTex;
float _SpriteFade;
float _Hologram_Value_1;
float _Hologram_Speed_1;
float _Outline_Size_1;
float4 _Outline_Color_1;
float _Outline_HDR_1;
float _Sub_Fade_1;
float _Add_Fade_1;

v2f vert(appdata_t IN)
{
v2f OUT;
OUT.vertex = UnityObjectToClipPos(IN.vertex);
OUT.texcoord = IN.texcoord;
OUT.color = IN.color;
return OUT;
}


float4 OutLine(float2 uv,sampler2D source, float value, float4 color, float HDR)
{

value*=0.01;
float4 mainColor = tex2D(source, uv + float2(-value, value))
+ tex2D(source, uv + float2(value, -value))
+ tex2D(source, uv + float2(value, value))
+ tex2D(source, uv - float2(value, value));

color *= HDR;
mainColor.rgb = color;
float4 addcolor = tex2D(source, uv);
if (mainColor.a > 0.40) { mainColor = color; }
if (addcolor.a > 0.40) { mainColor = addcolor; mainColor.a = addcolor.a; }
return mainColor;
}
inline float Holo1mod(float x,float modu)
{
return x - floor(x * (1.0 / modu)) * modu;
}

inline float Holo1noise(sampler2D source,float2 p)
{
float _TimeX = _Time.y;
float sample = tex2D(source,float2(.2,0.2*cos(_TimeX))*_TimeX*8. + p*1.).x;
sample *= sample;
return sample;
}

inline float Holo1onOff(float a, float b, float c)
{
float _TimeX = _Time.y;
return step(c, sin(_TimeX + a*cos(_TimeX*b)));
}

float4 Hologram(float2 uv, sampler2D source, float value, float speed)
{
float alpha = tex2D(source, uv).a;
float _TimeX = _Time.y * speed;
float2 look = uv;
float window = 1. / (1. + 20.*(look.y - Holo1mod(_TimeX / 4., 1.))*(look.y - Holo1mod(_TimeX / 4., 1.)));
look.x = look.x + sin(look.y*30. + _TimeX) / (50.*value)*Holo1onOff(4., 4., .3)*(1. + cos(_TimeX*80.))*window;
float vShift = .4*Holo1onOff(2., 3., .9)*(sin(_TimeX)*sin(_TimeX*20.) + (0.5 + 0.1*sin(_TimeX*20.)*cos(_TimeX)));
look.y = Holo1mod(look.y + vShift, 1.);
float4 video = float4(0, 0, 0, 0);
float4 videox = tex2D(source, look);
video.r = tex2D(source, look - float2(.05, 0.)*Holo1onOff(2., 1.5, .9)).r;
video.g = videox.g;
video.b = tex2D(source, look + float2(.05, 0.)*Holo1onOff(2., 1.5, .9)).b;
video.a = videox.a;
video = video;
float vigAmt = 3. + .3*sin(_TimeX + 5.*cos(_TimeX*5.));
float vignette = (1. - vigAmt*(uv.y - .5)*(uv.y - .5))*(1. - vigAmt*(uv.x - .5)*(uv.x - .5));
float noi = Holo1noise(source,uv*float2(0.5, 1.) + float2(6., 3.))*value * 3;
float y = Holo1mod(uv.y*4. + _TimeX / 2. + sin(_TimeX + sin(_TimeX*0.63)), 1.);
float start = .5;
float end = .6;
float inside = step(start, y) - step(end, y);
float fact = (y - start) / (end - start)*inside;
float f1 = (1. - fact) * inside;
video += f1*noi;
video += Holo1noise(source,uv*2.) / 2.;
video.r *= vignette;
video *= (12. + Holo1mod(uv.y*30. + _TimeX, 1.)) / 13.;
video.a = video.a + (frac(sin(dot(uv.xy*_TimeX, float2(12.9898, 78.233))) * 43758.5453))*.5;
video.a = (video.a*.3)*alpha*vignette * 2;
video.a *=1.2;
video.a *= 1.2;
video = lerp(tex2D(source, uv), video, value);
return video;
}
float DSFXr (float2 c, float seed)
{
return frac(43.*sin(c.x+7.*c.y)*seed);
}

float DSFXn (float2 p, float seed)
{
float2 i = floor(p), w = p-i, j = float2 (1.,0.);
w = w*w*(3.-w-w);
return lerp(lerp(DSFXr(i, seed), DSFXr(i+j, seed), w.x), lerp(DSFXr(i+j.yx, seed), DSFXr(i+1., seed), w.x), w.y);
}

float DSFXa (float2 p, float seed)
{
float m = 0., f = 2.;
for ( int i=0; i<9; i++ ){ m += DSFXn(f*p, seed)/f; f+=f; }
return m;
}

float4 DestroyerFX(float4 txt, float2 uv, float value, float seed, float HDR)
{
float t = frac(value*0.9999);
float4 c = smoothstep(t / 1.2, t + .1, DSFXa(3.5*uv, seed));
c = txt*c;
c.r = lerp(c.r, c.r*120.0*(1 - c.a), value);
c.g = lerp(c.g, c.g*40.0*(1 - c.a), value);
c.b = lerp(c.b, c.b*5.0*(1 - c.a) , value);
c.rgb = lerp(saturate(c.rgb),c.rgb,HDR);
return c;
}
float4 frag (v2f i) : COLOR
{
float4 _Hologram_1 = Hologram(i.texcoord,_MainTex,_Hologram_Value_1,_Hologram_Speed_1);
float4 _Outline_1 = OutLine(i.texcoord,_MainTex,_Outline_Size_1,_Outline_Color_1,_Outline_HDR_1);
float4 SourceRGBA_1 = tex2D(_MainTex, i.texcoord);
_Outline_1 = lerp(_Outline_1,_Outline_1 - SourceRGBA_1,_Sub_Fade_1);
_Hologram_1 = lerp(_Hologram_1,_Hologram_1*_Hologram_1.a + _Outline_1*_Outline_1.a,_Add_Fade_1);
float4 FinalResult = _Hologram_1;
FinalResult.rgb *= i.color.rgb;
FinalResult.a = FinalResult.a * _SpriteFade * i.color.a;
return FinalResult;
}

ENDCG
}
}
Fallback "Sprites/Default"
}
