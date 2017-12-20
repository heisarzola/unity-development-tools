using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEditor.SceneManagement;


namespace FavouritesEd
{
	public class FavouritesTreeView: TreeViewWithTreeModel<FavouritesTreeElement>
	{
		private static readonly GUIContent GC_None = new GUIContent("There are currently no categories to drag favorites into.");
		private static readonly string DragAndDropID = "FavouritesTreeElement";

		public TreeModel<FavouritesTreeElement> Model { get { return model; } }
		private TreeModel<FavouritesTreeElement> model;

		private FavouritesAsset asset;

		// ------------------------------------------------------------------------------------------------------------

		public FavouritesTreeView(TreeViewState treeViewState) 
			: base(treeViewState)
		{
			baseIndent = 5f;
		}

		public void OnGUI()
		{
			if (model != null && model.Data != null && model.Data.Count > 1)
			{
				base.OnGUI(GUILayoutUtility.GetRect(1, 1, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true)));
			}
			else
			{
				GUILayout.Label(GC_None);
				GUILayout.FlexibleSpace();
			}
		}

		public void LoadAndUpdate(FavouritesAsset favsAsset = null)
		{
			if (favsAsset != null) asset = favsAsset;

			// add root
			FavouritesTreeElement treeRoot = new FavouritesTreeElement() { ID = 0, Depth = -1, Name = "Root" };
			model = new TreeModel<FavouritesTreeElement>(new List<FavouritesTreeElement>() { treeRoot });

			// add categories
			List<FavouritesTreeElement> categories = new List<FavouritesTreeElement>();
			Texture2D icon = EditorGUIUtility.IconContent(FolderIconName()).image as Texture2D;
			foreach (FavouritesCategory c in asset.categories)
			{
				FavouritesTreeElement ele = new FavouritesTreeElement()
				{
					Name = c.name,
					Icon = icon,
					ID = model.GenerateUniqueID(),
					category = c
				};

				categories.Add(ele);
				model.QuickAddElement(ele, treeRoot);
			}

			// add favourites from project and scene(s)
			List<FavouritesElement> favs = new List<FavouritesElement>();
			favs.AddRange(asset.favs);

			// add from scene(s)
			foreach (FavouritesContainer c in FavouritesEditor.Containers)
			{
				if (c == null || c.favs == null) continue;
				favs.AddRange(c.favs);
			}

			// sort
			favs.Sort((a, b) => 
			{
				int r = a.categoryId.CompareTo(b.categoryId);
				if (r == 0 && a.obj != null && b.obj != null) r = a.obj.name.CompareTo(b.obj.name);
				return r;
			});

			// and add to tree
			foreach (FavouritesElement ele in favs)
			{
				if (ele == null || ele.obj == null) continue;
				foreach (FavouritesTreeElement c in categories)
				{
					if (c.category.id == ele.categoryId)
					{
						string nm = ele.obj.name;
						GameObject go = ele.obj as GameObject;
						if (go != null && go.scene.IsValid())
						{
							nm = string.Format("{0} ({1})", nm, go.scene.name);
						}

						icon = AssetPreview.GetMiniTypeThumbnail(ele.obj.GetType());

						model.QuickAddElement(new FavouritesTreeElement()
						{
							Name = nm,
							Icon = icon,
							ID = model.GenerateUniqueID(),
							fav = ele
						}, c);

						break;
					}
				}
			}

			model.UpdateDataFromTree();
			Init(model);
			Reload();
			SetSelection(new List<int>());
		}		

		protected override void RowGUI(RowGUIArgs args)
		{
			base.RowGUI(args);
		}

		protected override void ContextClickedItem(int id)
		{
			GenericMenu menu = new GenericMenu();
			menu.AddItem(new GUIContent("Ping"), false, HandleContextOption, id);
			menu.ShowAsContext();
		}

		private void HandleContextOption(object arg)
		{
			int id = (int)arg;
			FavouritesTreeElement ele = Model.Find(id);
			if (ele != null && ele.fav != null && ele.fav.obj != null)
			{
				EditorGUIUtility.PingObject(ele.fav.obj);
			}
		}

		protected override void DoubleClickedItem(int id)
		{
			FavouritesTreeElement ele = Model.Find(id);
			if (ele != null && ele.fav != null && ele.fav.obj != null)
			{
				AssetDatabase.OpenAsset(ele.fav.obj);				
			}
			else
			{
				SetExpanded(id, !IsExpanded(id));				
			}
		}

		protected override bool CanMultiSelect(TreeViewItem item)
		{
			return false;
		}


		protected override bool CanStartDrag(CanStartDragArgs args)
		{
			if (asset == null || asset.categories.Count == 0 || 
				!rootItem.hasChildren || args.draggedItem.parent == rootItem)
			{
				return false;
			}

			return true;
		}

		protected override void SetupDragAndDrop(SetupDragAndDropArgs args)
		{
			if (args.draggedItemIDs.Count == 0) return;

			FavouritesTreeElement item = Model.Find(args.draggedItemIDs[0]);
			if (item == null || item.fav == null || item.fav.obj == null) return;

			DragAndDrop.PrepareStartDrag();
			DragAndDrop.SetGenericData(DragAndDropID, item);
			DragAndDrop.objectReferences = new Object[] { item.fav.obj };
			DragAndDrop.StartDrag(item.fav.obj.name);
		}

		protected override DragAndDropVisualMode HandleDragAndDrop(DragAndDropArgs args)
		{
			if (asset == null || asset.categories.Count == 0 || !rootItem.hasChildren)
			{
				return DragAndDropVisualMode.Rejected;
			}

			if (args.performDrop)
			{
				FavouritesTreeElement ele;
				int id = args.parentItem == null ? -1 : args.parentItem.id;
				if (id < 0 || (ele = model.Find(id)) == null || ele.category == null)
				{
					IList<int> ids = GetSelection();
					if (ids.Count > 0)
					{
						TreeViewItem item = FindItem(ids[0], rootItem);
						if (item == null) return DragAndDropVisualMode.Rejected;
						id = item.parent == rootItem ? item.id : item.parent.id;
					}
					else
					{
						id = rootItem.children[0].id;
					}
					ele = model.Find(id);
				}

				if (ele == null || ele.category == null)
				{
					return DragAndDropVisualMode.Rejected;
				}

				int categoryId = ele.category.id;

				// first check if it is "internal" drag drop from one category to another
				FavouritesTreeElement draggedEle = DragAndDrop.GetGenericData(DragAndDropID) as FavouritesTreeElement;
				if (draggedEle != null)
				{
					draggedEle.fav.categoryId = categoryId;

					// check if in scene and mark scene dirty, else do nothing
					// more since asset is marked dirty at end anyway
					GameObject go = draggedEle.fav.obj as GameObject;
					if (go != null && go.scene.IsValid())
					{
						EditorSceneManager.MarkSceneDirty(go.scene);
					}
				}

				// else the drag-drop originated somewhere else
				else
				{					
					Object[] objs = DragAndDrop.objectReferences;
					foreach (Object obj in objs)
					{
						// check if in scene
						GameObject go = obj as GameObject;
						if (go != null && go.scene.IsValid())
						{
							AddToSceneFavs(go, categoryId);
							continue;
						}

						// make sure it is not a component
						if ((obj as Component) != null) continue;

						// else, probably something from project panel
						asset.favs.Add(new FavouritesElement()
						{
							obj = obj,
							categoryId = categoryId
						});
					}
				}
				
				EditorUtility.SetDirty(asset);
				LoadAndUpdate();
			}

			return DragAndDropVisualMode.Generic;
		}

		// ------------------------------------------------------------------------------------------------------------

		private void AddToSceneFavs(GameObject go, int categoryId)
		{
			FavouritesContainer container = FavouritesEditor.GetContainer(go.scene);
			if (container == null) return; // just in case

			container.favs.Add(new FavouritesElement()
			{
				categoryId = categoryId,
				obj = go
			});

			EditorSceneManager.MarkSceneDirty(go.scene);
		}

		private static System.Func<string> Invoke_folderIconName;

		private static string FolderIconName()
		{
			if (Invoke_folderIconName == null)
			{
				Assembly asm = Assembly.GetAssembly(typeof(Editor));
				PropertyInfo prop = asm.GetType("UnityEditorInternal.EditorResourcesUtility").GetProperty("folderIconName", (BindingFlags.Static | BindingFlags.Public));
				MethodInfo method = prop.GetGetMethod(true);
				Invoke_folderIconName = (System.Func<string>)System.Delegate.CreateDelegate(typeof(System.Func<string>), method);
			}
			return Invoke_folderIconName();
		}

		// ------------------------------------------------------------------------------------------------------------
	}
}
