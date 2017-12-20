using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEditor.SceneManagement;

namespace FavouritesEd
{
    public class FavouritesWindow : EditorWindow
    {
        private static readonly GUIContent GC_Add = new GUIContent("New Category", "Add category");
        private static readonly GUIContent GC_Remove = new GUIContent("Remove Selected", "Remove selected");

        [SerializeField] private FavouritesAsset asset;
        [SerializeField] private TreeViewState treeViewState;
        [SerializeField] private FavouritesTreeView treeView;
        [SerializeField] private SearchField searchField;

        // ------------------------------------------------------------------------------------------------------------------

        [MenuItem("Tools/Project/Favorite Assets")]
        private static void ShowWindow()
        {
            GetWindow<FavouritesWindow>("Favorite Assets").UpdateTreeview();
        }

        private void OnHierarchyChange()
        {
            UpdateTreeview();
        }

        private void OnProjectChange()
        {
            UpdateTreeview();
        }

        private void UpdateTreeview()
        {
            if (asset == null)
            {
                LoadAsset();
            }

            if (treeViewState == null)
                treeViewState = new TreeViewState();

            if (treeView == null)
            {
                searchField = null;
                treeView = new FavouritesTreeView(treeViewState);
            }

            if (searchField == null)
            {
                searchField = new SearchField();
                searchField.downOrUpArrowKeyPressed += treeView.SetFocusAndEnsureSelectedItem;
            }

            treeView.LoadAndUpdate(asset);
            Repaint();
        }

        // ------------------------------------------------------------------------------------------------------------------

        private void OnGUI()
        {
            if (treeView == null)
            {
                UpdateTreeview();
            }

            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
            {
                treeView.searchString = searchField.OnToolbarGUI(treeView.searchString, GUILayout.ExpandWidth(true));
                GUILayout.Space(5);
                if (GUILayout.Button(GC_Add, EditorStyles.toolbarButton, GUILayout.Width(80)))
                {
                    TextInputWindow.ShowWindow("Favorite Assets", "Enter category name", "", AddCategory, null);
                }
                GUI.enabled = treeView.Model.Data.Count > 0;
                if (GUILayout.Button(GC_Remove, EditorStyles.toolbarButton, GUILayout.Width(95)) && EditorUtility.DisplayDialog("Are you sure?", "Are you sure you want to delete the references to the selected items?\n\nIf a category was selected, the items within it will be deleted as well.", "Yes, I'm Sure.", "Changed My Mind."))
                {
                    RemoveSelected();
                }
                GUI.enabled = true;
            }
            EditorGUILayout.EndHorizontal();

            treeView.OnGUI();
        }

        // ------------------------------------------------------------------------------------------------------------------

        private void AddCategory(TextInputWindow wiz)
        {
            string s = wiz.Text;
            wiz.Close();
            if (string.IsNullOrEmpty(s)) return;

            asset.AddCategory(s);
            EditorUtility.SetDirty(asset);

            UpdateTreeview();
            Repaint();
        }

        private void RemoveSelected()
        {
            IList<int> ids = treeView.GetSelection();
            if (ids.Count == 0) return;

            FavouritesTreeElement ele = treeView.Model.Find(ids[0]);
            if (ele == null) return;

            if (ele.category != null)
            {
                // remove elements from open scene. those in closed scenes will just
                // have to stay. they will not show up anyway if category is gone

                // remove from scene
                foreach (FavouritesContainer c in FavouritesEditor.Containers)
                {
                    if (c == null || c.favs == null) continue;
                    for (int i = c.favs.Count - 1; i >= 0; i--)
                    {
                        if (c.favs[i].categoryId == ele.category.id)
                        {
                            c.favs.RemoveAt(i);
                            EditorSceneManager.MarkSceneDirty(c.gameObject.scene);
                        }
                    }
                }

                // remove favourites linked to this category
                for (int i = asset.favs.Count - 1; i >= 0; i--)
                {
                    if (asset.favs[i].categoryId == ele.category.id) asset.favs.RemoveAt(i);
                }

                // remove category
                for (int i = 0; i < asset.categories.Count; i++)
                {
                    if (asset.categories[i].id == ele.category.id)
                    {
                        asset.categories.RemoveAt(i);
                        break;
                    }
                }

                EditorUtility.SetDirty(asset);
            }
            else
            {
                bool found = false;
                for (int i = 0; i < asset.favs.Count; i++)
                {
                    if (asset.favs[i] == ele.fav)
                    {
                        found = true;
                        asset.favs.RemoveAt(i);
                        EditorUtility.SetDirty(asset);
                        break;
                    }
                }

                if (!found)
                {
                    foreach (FavouritesContainer c in FavouritesEditor.Containers)
                    {
                        if (c == null || c.favs == null) continue;
                        for (int i = 0; i < c.favs.Count; i++)
                        {
                            if (c.favs[i] == ele.fav)
                            {
                                found = true;
                                c.favs.RemoveAt(i);
                                EditorSceneManager.MarkSceneDirty(c.gameObject.scene);
                                break;
                            }
                        }
                        if (found) break;
                    }
                }
            }

            UpdateTreeview();
            Repaint();
        }

        private FavouritesAsset LoadAsset()
        {
            string[] guids = AssetDatabase.FindAssets("t:FavouritesAsset");
            string fn = (guids.Length > 0 ? AssetDatabase.GUIDToAssetPath(guids[0]) : GetPackageFolder() + "FavouritesAsset.asset");
            asset = AssetDatabase.LoadAssetAtPath<FavouritesAsset>(fn);
            if (asset == null)
            {
                asset = CreateInstance<FavouritesAsset>();
                AssetDatabase.CreateAsset(asset, fn);
                AssetDatabase.SaveAssets();
            }

            return asset;
        }

        private string GetPackageFolder()
        {
            try
            {
                string[] res = System.IO.Directory.GetFiles(Application.dataPath, "FavouritesEdWindow.cs", System.IO.SearchOption.AllDirectories);
                if (res.Length > 0) return "Assets" + res[0].Replace(Application.dataPath, "").Replace("FavouritesEdWindow.cs", "").Replace("\\", "/");
            }
            catch (System.Exception ex)
            {
                Debug.LogException(ex);
            }
            return "Assets/";
        }

        // ------------------------------------------------------------------------------------------------------------------
    }
}