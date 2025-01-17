using UnityEditor;
using UnityEngine;

namespace Assets.Src.Code.Editor
{
    public class CreateAssetBundles : MonoBehaviour
    {
        [MenuItem("Asset/Create assets bundles")]
        private static void BuildAllAssetBundles()
        {
            string assetBundleDirPath = Application.dataPath + "/../AssetsBundles";

            try
            {
                BuildPipeline.BuildAssetBundles(
                    assetBundleDirPath, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
            }
            catch (System.Exception e)
            {
                Debug.LogWarning(e);
            }
        }
    }
}