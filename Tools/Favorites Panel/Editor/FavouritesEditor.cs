using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;


namespace FavouritesEd
{
	[InitializeOnLoad]
	public static class FavouritesEditor
	{
		public static readonly string ContainerObjName = "_FavouritesEd_K2KJB0WK4P3B_";
		private static Dictionary<Scene, FavouritesContainer> cache = null;

		// ------------------------------------------------------------------------------------------------------------------

		static FavouritesEditor()
		{
			EditorSceneManager.sceneOpened += SceneOpened;
			EditorSceneManager.sceneClosed += SceneClosed;
		}

		public static IEnumerable<FavouritesContainer> Containers
		{
			get
			{
				UpdateSceneCache(false);
				return cache.Values;
			}
		}

		public static FavouritesContainer GetContainer(Scene scene)
		{
			UpdateSceneCache(false);

			FavouritesContainer c;
			if (cache.TryGetValue(scene, out c)) return c;

			GameObject go = new GameObject(ContainerObjName)
			{
				tag = "EditorOnly",
				hideFlags = HideFlags.HideInHierarchy
			};

			c = go.AddComponent<FavouritesContainer>();
			if (go.scene != scene) SceneManager.MoveGameObjectToScene(go, scene);			
			EditorSceneManager.MarkSceneDirty(scene);

			cache.Add(scene, c);

			return c;
		}

		public static void UpdateSceneCache(bool force)
		{
			if (cache == null) cache = new Dictionary<Scene, FavouritesContainer>();
			else if (!force) return;

			Object[] objs = Object.FindObjectsOfType<FavouritesContainer>();
			foreach (FavouritesContainer c in objs)
			{
				if (!cache.ContainsKey(c.gameObject.scene)) cache.Add(c.gameObject.scene, c);
			}
		}

		// ------------------------------------------------------------------------------------------------------------------

		private static void SceneOpened(Scene scene, OpenSceneMode mode)
		{
			UpdateSceneCache(true);
		}

		private static void SceneClosed(Scene scene)
		{
			UpdateSceneCache(false);
			if (cache.ContainsKey(scene)) cache.Remove(scene);
		}

		// ------------------------------------------------------------------------------------------------------------------
	}
}