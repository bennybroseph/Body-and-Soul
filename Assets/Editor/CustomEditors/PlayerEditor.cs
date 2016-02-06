using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Player), true)]
[CanEditMultipleObjects]
public class PlayerEditor : ActorEditor
{

    protected override void OnEnable()
    {
        base.OnEnable();

        Properties.AddProperty("m_HitPoints", 0);
    }
}
