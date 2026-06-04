using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public static class SetupYSorting
{
    private const string TargetLayer = "coisas";

    private static readonly string[] PropKeywords = {
        "Mesa", "Vaso", "Estante", "Bebedouro", "Tapete"
    };

    [MenuItem("Tools/Setup Y Sorting em Cenas de Fase")]
    public static void Run()
    {
        string[] scenePaths = {
            "Assets/Scenes/Phase1_Office.unity",
            "Assets/Scenes/Phase2_Office.unity"
        };

        int total = 0;

        foreach (string path in scenePaths)
        {
            Scene scene = EditorSceneManager.OpenScene(path, OpenSceneMode.Additive);

            foreach (GameObject go in scene.GetRootGameObjects())
                total += ProcessHierarchy(go);

            EditorSceneManager.SaveScene(scene);
            EditorSceneManager.CloseScene(scene, true);
        }

        Debug.Log($"[SetupYSorting] {total} SpriteRenderer(s) atualizados para layer '{TargetLayer}' com SpriteSortPoint = Pivot.");
    }

    private static int ProcessHierarchy(GameObject root)
    {
        int count = 0;

        foreach (Transform t in root.GetComponentsInChildren<Transform>(true))
        {
            if (!IsProp(t.name)) continue;

            var sr = t.GetComponent<SpriteRenderer>();
            if (sr == null) continue;

            Undo.RecordObject(sr, "SetupYSorting");
            sr.sortingLayerName = TargetLayer;
            sr.spriteSortPoint = SpriteSortPoint.Pivot;
            EditorUtility.SetDirty(sr);

            AdjustCollider(t, sr);
            count++;
        }

        return count;
    }

    // Reduz o BoxCollider2D para cobrir só a base do sprite (30% inferior),
    // que é a parte que fisicamente bloqueia o player num jogo top-down.
    private static void AdjustCollider(Transform t, SpriteRenderer sr)
    {
        var col = t.GetComponent<BoxCollider2D>();
        if (col == null || sr.sprite == null) return;

        float spriteHeight = sr.sprite.bounds.size.y;
        float spriteWidth  = sr.sprite.bounds.size.x;

        float colliderHeight = spriteHeight * 0.30f;
        float offsetY        = colliderHeight / 2f;  // pivot está na base, então sobe meio collider

        Undo.RecordObject(col, "SetupYSorting");
        col.size   = new Vector2(spriteWidth * 0.85f, colliderHeight);
        col.offset = new Vector2(0f, offsetY);
        EditorUtility.SetDirty(col);
    }

    private static bool IsProp(string name)
    {
        foreach (var kw in PropKeywords)
            if (name.IndexOf(kw, System.StringComparison.OrdinalIgnoreCase) >= 0)
                return true;
        return false;
    }
}
