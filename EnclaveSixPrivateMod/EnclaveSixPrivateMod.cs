using System;
using System.IO;
using BepInEx;
using Jotunn;
using Jotunn.Managers;
using Jotunn.Utils;
using UnityEngine;

namespace EnclaveSixPrivateMod
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    [BepInDependency(Main.ModGuid)]
    [NetworkCompatibility(CompatibilityLevel.NotEnforced, VersionStrictness.None)]
    internal class EnclaveSixPrivateMod : BaseUnityPlugin
    {
        public const string PluginGuid = "com.chebgonaz.enclavesixprivatemod";
        public const string PluginName = "EnclaveSixPrivateMod";
        public const string PluginVersion = "1.0.0";

        private Mesh _bannerMesh;
        private Material _bannerMaterial;

        private void Awake()
        {
            LoadAssetBundle();

            PrefabManager.OnVanillaPrefabsAvailable += DoOnVanillaPrefabsAvailable;
        }

        private void DoOnVanillaPrefabsAvailable()
        {
            PrefabManager.OnVanillaPrefabsAvailable -= DoOnVanillaPrefabsAvailable;

            var blackBannerPrefab = PrefabManager.Instance.GetPrefab("piece_banner01");
            if (blackBannerPrefab == null)
            {
                Logger.LogError("Failed to get piece_banner01 prefab");
                return;
            }

            var child = blackBannerPrefab.transform.Find("default");
            if (child == null)
            {
                Logger.LogError("child is null; banner swap failed");
                return;
            }

            var meshFilter = child.GetComponentInChildren<MeshFilter>(true);
            if (meshFilter == null)
            {
                Logger.LogError("Failed to get banner prefab's mesh filter");
                return;
            }
            
            meshFilter.mesh = _bannerMesh;

            var meshRenderer = child.GetComponentInChildren<MeshRenderer>(true);
            if (meshRenderer == null)
            {
                Logger.LogError("Failed to get banner prefab's mesh renderer");
                return;
            }
            
            meshRenderer.sharedMaterial = _bannerMaterial;
        }

        private void LoadAssetBundle()
        {
            var assetBundlePath = Path.Combine(Path.GetDirectoryName(Info.Location), "enclavesixprivatemod");
            var chebgonazAssetBundle = AssetUtils.LoadAssetBundle(assetBundlePath);
            try
            {
                _bannerMesh = chebgonazAssetBundle.LoadAsset<Mesh>("default");
                _bannerMaterial = chebgonazAssetBundle.LoadAsset<Material>("Dragon");
            }
            catch (Exception ex)
            {
                Logger.LogWarning($"Exception caught while loading assets: {ex}");
            }
            finally
            {
                chebgonazAssetBundle.Unload(false);
            }
        }
    }
}