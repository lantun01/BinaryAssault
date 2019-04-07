using UnityEngine;
using UnityEditor;
using System.Collections;
using _ShaderoShaderEditorFramework;
using _ShaderoShaderEditorFramework.Utilities;

namespace _ShaderoShaderEditorFramework
{
[Node(false, "Initialize/Source Render Texture")]

public class SourceRenderTexture : Node
{
    public const string ID = "SourceRenderTexture";
    public override string GetID { get { return ID; } }
    [HideInInspector]
    public RenderTexture tex;
    public static int count = 1;
    public static bool tag = false;
    public static string code;

    public static void Init()
    {
        tag = false;
        count = 1;
    }
    public override Node Create(Vector2 pos)
    {
        SourceRenderTexture node = ScriptableObject.CreateInstance<SourceRenderTexture>();

        node.name = "Source Render Texture";
        node.rect = new Rect(pos.x, pos.y, 148, 210);
        node.CreateOutput("Source", "SuperSource");

        return node;
    }

    protected internal override void NodeGUI()
    {
        Outputs[0].DisplayLayout(new GUIContent("Source", "Source"));
        tex = (RenderTexture)EditorGUI.ObjectField(new Rect(8, 30, 130, 130), tex, typeof(RenderTexture),true);
        if (NodeEditor._Shadero_Material != null)
        {
            if (tex!=null)
            { 
            NodeEditor._Shadero_Material.SetTexture(FinalVariable, tex);
            }
        }
    }

    private string FinalVariable;
    [HideInInspector]
    public int MemoCount = -1;
    public override bool FixCalculate()
    {
        MemoCount = count;
        count++;
        return true;
    }

    public override bool Calculate()
    {
      

        SuperSource s_out = new SuperSource();

        string NodeCount = MemoCount.ToString();
        string DefaultName = "_SourceRenderTextureTex_" + NodeCount;
      
        s_out.StringPreviewNew = s_out.ValueLine;
        s_out.Result = DefaultName;
        FinalVariable = DefaultName;

        s_out.ParametersLines = DefaultName + "(\"" + DefaultName + "(RGB)\", 2D) = \"white\" { }\n";
        s_out.ParametersDeclarationLines = "sampler2D " + DefaultName + ";\n";


        Outputs[0].SetValue<SuperSource>(s_out);
        count++;
        return true;
    }
}
}