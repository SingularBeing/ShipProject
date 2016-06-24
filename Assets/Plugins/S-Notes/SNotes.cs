#if UNITY_EDITOR
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEditor;
using System;
using SimpleJSON;

[System.Serializable]
public class SNotes : EditorWindow
{
	//trello
	private string url = "http://trello.com/b/example", importButtonText = "Import", key;

	private Vector2 scrollPosition;
	public string notes;
	public int points;
	public int curCategory;
	public int curItem;
	public bool submit = true;
	public bool rewardsView, importTrelloView;

	public List<ListCategories> categories = new List<ListCategories> ();

	public List<ListRewards> rewards = new List<ListRewards> ();

	public Rect windowRect = new Rect (100, 0, 200, 200);

	[System.Serializable]
	public class ListRewards
	{
		public int pointsNeeded;
		public string rewardName;
		public bool editing;
	}

	[System.Serializable]
	public class ListCategories
	{
		public bool editName, showExpanded;
		public string catName;
		public List<ListItem> items = new List<ListItem> ();
	}

	[System.Serializable]
	public class ListItem
	{
		public bool completed, editName, expanded, redeemed;
		public string assigned, itemDescription;
		public string due;
		public string title;
		public int points;
		public Color color;
	}

	void OnEnable ()
	{
		importButtonText = "Import";
		points = EditorPrefs.GetInt ("SNotesPoints");
		url = EditorPrefs.GetString ("SNotesTrelloURL");
		key = EditorPrefs.GetString ("SNotesTrelloKey");

		//load
		if (rewards.Count != EditorPrefs.GetInt ("SNotesRewardsSize")) {
			for (int i = 0; i < EditorPrefs.GetInt ("SNotesRewardsSize"); i++) {
				rewards.Add (new ListRewards ());
				rewards [i].rewardName = EditorPrefs.GetString ("SNotesRewardsName" + i);
				rewards [i].pointsNeeded = EditorPrefs.GetInt ("SNotesRewardsPoints" + i);
			}
		}

		if (categories.Count != EditorPrefs.GetInt ("SNotesCategorySize")) {
			for (int i = 0; i < EditorPrefs.GetInt ("SNotesCategorySize"); i++) {
				categories.Add (new ListCategories ());
				categories [i].catName = EditorPrefs.GetString ("SNotesCategoryNames" + i);
				for (int x = 0; x < EditorPrefs.GetInt ("SNotesItemCat" + i + "ItemCount"); x++) {
					categories [i].items.Add (new ListItem ());
					categories [i].items [x].title = EditorPrefs.GetString ("SNotesItemCat" + i + "Item" + x + "Title");
					categories [i].items [x].itemDescription = EditorPrefs.GetString ("SNotesItemCat" + i + "Item" + x + "Desc");
					categories [i].items [x].due = EditorPrefs.GetString ("SNotesItemCat" + i + "Item" + x + "Due");
					categories [i].items [x].assigned = EditorPrefs.GetString ("SNotesItemCat" + i + "Item" + x + "Assigned");
					categories [i].items [x].points = EditorPrefs.GetInt ("SNotesItemCat" + i + "Item" + x + "Points");
					categories [i].items [x].redeemed = EditorPrefs.GetBool ("SNotesItemCat" + i + "Item" + x + "Redeemed");
					categories [i].items [x].completed = EditorPrefs.GetBool ("SNotesItemCat" + i + "Item" + x + "Completed");
					categories [i].items [x].color.r = EditorPrefs.GetFloat ("SNotesItemCat" + i + "Item" + x + "ColorR");
					categories [i].items [x].color.g = EditorPrefs.GetFloat ("SNotesItemCat" + i + "Item" + x + "ColorG");
					categories [i].items [x].color.b = EditorPrefs.GetFloat ("SNotesItemCat" + i + "Item" + x + "ColorB");
					categories [i].items [x].color.a = EditorPrefs.GetFloat ("SNotesItemCat" + i + "Item" + x + "ColorA");
				}
			}
		}
	}

	void OnDisable ()
	{
		EditorPrefs.SetInt ("SNotesPoints", points);
		EditorPrefs.SetString ("SNotesTrelloURL", url);
		EditorPrefs.SetString ("SNotesTrelloKey", key);

		//save
		EditorPrefs.SetInt ("SNotesRewardsSize", rewards.Count);
		for (int i = 0; i < rewards.Count; i++) {
			EditorPrefs.SetString ("SNotesRewardsName" + i, rewards [i].rewardName);
			EditorPrefs.SetInt ("SNotesRewardsPoints" + i, rewards [i].pointsNeeded);
		}

		EditorPrefs.SetInt ("SNotesCategorySize", categories.Count);
		for (int i = 0; i < categories.Count; i++) {
			EditorPrefs.SetString ("SNotesCategoryNames" + i, categories [i].catName);
			EditorPrefs.SetInt ("SNotesItemCat" + i + "ItemCount", categories [i].items.Count);
			for (int x = 0; x < categories [i].items.Count; x++) {
				EditorPrefs.SetString ("SNotesItemCat" + i + "Item" + x + "Title", categories [i].items [x].title);
				EditorPrefs.SetString ("SNotesItemCat" + i + "Item" + x + "Desc", categories [i].items [x].itemDescription);
				EditorPrefs.SetString ("SNotesItemCat" + i + "Item" + x + "Due", categories [i].items [x].due);
				EditorPrefs.SetString ("SNotesItemCat" + i + "Item" + x + "Assigned", categories [i].items [x].assigned);
				EditorPrefs.SetInt ("SNotesItemCat" + i + "Item" + x + "Points", categories [i].items [x].points);
				EditorPrefs.SetBool ("SNotesItemCat" + i + "Item" + x + "Redeemed", categories [i].items [x].redeemed);
				EditorPrefs.SetBool ("SNotesItemCat" + i + "Item" + x + "Completed", categories [i].items [x].completed);
				EditorPrefs.SetFloat ("SNotesItemCat" + i + "Item" + x + "ColorR", categories [i].items [x].color.r);
				EditorPrefs.SetFloat ("SNotesItemCat" + i + "Item" + x + "ColorG", categories [i].items [x].color.g);
				EditorPrefs.SetFloat ("SNotesItemCat" + i + "Item" + x + "ColorB", categories [i].items [x].color.b);
				EditorPrefs.SetFloat ("SNotesItemCat" + i + "Item" + x + "ColorA", categories [i].items [x].color.a);
			}
		}
	}

	//display window and add a menu item
	[MenuItem ("Tools/SNotes")]
	public static void Init ()
	{
		EditorWindow window = EditorWindow.GetWindow (typeof(SNotes));
		window.autoRepaintOnSceneChange = true;
		window.ShowUtility ();
		window.Repaint ();
	}

	public void OnGUI ()
	{
		if (Input.GetMouseButtonDown (0))
			GUI.SetNextControlName ("");
		//scrollbars
		scrollPosition = GUILayout.BeginScrollView (scrollPosition, true, true, GUILayout.Width (position.width), GUILayout.Height (position.height));
		GUILayout.Space (10);

		GUILayout.Space (10);
		GUILayout.BeginArea (new Rect (0 + scrollPosition.x, 0, this.position.size.x, 20));
		{
			//rewards and notes buttons
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button ("Notes")) {
				rewardsView = false;
				importTrelloView = false;
			}
			GUILayout.Space (05);
			if (GUILayout.Button ("Rewards")) {
				rewardsView = true;
				importTrelloView = false;
			}
			GUILayout.Space (05);
			//trello integration
			if (GUILayout.Button ("Import Trello")) {
				rewardsView = false;
				importTrelloView = true;
			}

			GUILayout.EndHorizontal ();
		}
		GUILayout.EndArea ();
		GUILayout.Space (5);



		//TRELLO stuffs
		if (importTrelloView) {
			EditorGUILayout.HelpBox ("Make sure that the board is set to Public" + "\n" + "It may not work if the current platform is Web Player", MessageType.Warning, true);
			//Input for player info
			GUILayout.Space (10);
			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Link to your board", GUILayout.Width (120));
			url = EditorGUILayout.TextArea (url, GUILayout.Height (20), GUILayout.Width (250));
			GUILayout.EndHorizontal ();
			EditorGUILayout.HelpBox ("Go to the menu in your board and press 'Share, Print, and Export...'" + "\n" + "Copy the link that shows up and paste it above.", MessageType.Info, true);
			GUILayout.Space (25);
			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Key", GUILayout.Width (120));
			key = EditorGUILayout.TextArea (key, GUILayout.Height (20), GUILayout.Width (250));
			GUILayout.EndHorizontal ();
			EditorGUILayout.HelpBox ("Copy and paste the 32 character code.", MessageType.Info, true);
            
			if (GUILayout.Button ("Get Key", GUILayout.Width (370))) {
				Application.OpenURL ("https://trello.com/1/appKey/generate");
			}
			GUILayout.Space (8);

			if (GUILayout.Button (importButtonText, GUILayout.Width (370))) {
				if (importButtonText == "Import")
					Import ();
			}
			if (GUILayout.Button ("Visit Trello.com", GUILayout.Width (370))) {
				Application.OpenURL ("http://trello.com/");
			}
			GUILayout.Space (50);
		}

		if (!importTrelloView)
            //rewards or notes
            if (rewardsView) {
			//create and remove rewards
			EditorGUILayout.BeginHorizontal ();
			if (GUILayout.Button ("Add new reward", GUILayout.Width (170))) {
				rewards.Add (new ListRewards ());
				rewards [rewards.Count - 1].rewardName = "Reward " + (rewards.Count - 1).ToString ();
			}
			EditorGUILayout.EndHorizontal ();

			//every reward
			for (int i = 0; i < rewards.Count; i++) {
				GUILayout.Space (5);
				GUILayout.BeginHorizontal ();
				GUILayout.Space (20);
				if (GUILayout.Button ("Purchase", GUILayout.Width (80))) {
					if (points > rewards [i].pointsNeeded) {
						points -= rewards [i].pointsNeeded;
						return;
					} else {
						Debug.Log ("[SNotes] Not enough points! Go do some more work.");
						return;
					}
				}
				GUILayout.Space (10);
				if (rewards [i].editing) {
					if (Event.current.Equals (Event.KeyboardEvent ("return"))) {
						rewards [i].rewardName = rewards [i].rewardName;
						rewards [i].editing = false;
					}
					rewards [i].rewardName = GUILayout.TextArea (rewards [i].rewardName, GUILayout.Width (200));
				} else {
					if (GUILayout.Button (rewards [i].rewardName, GUILayout.Width (200)))
						rewards [i].editing = true;
				}
				GUILayout.Space (2);
				//set the amount of points
				GUILayout.Label ("Points ", GUILayout.Width (40));
				rewards [i].pointsNeeded = EditorGUILayout.IntField (rewards [i].pointsNeeded, GUILayout.Width (40));
				GUILayout.Space (5);
				//remove button
				if (GUILayout.Button ("Remove", GUILayout.Width (80))) {
					rewards.Remove (rewards [i]);
					return;
				}
				GUILayout.EndHorizontal ();
			}
		} else {
			//every category
			EditorGUILayout.BeginHorizontal ();
			GUILayout.Space (20);
			BeginWindows ();
			for (int i = 0; i < categories.Count; i++) {
				windowRect.x = i * 400;
				windowRect.x += 10;
				windowRect.y = 50;
				windowRect.size = new Vector2 (380, categories [i].items.Count * 20);
				windowRect = GUILayout.Window (i, windowRect, CategoryWindows, categories [i].catName);
			}
			EndWindows ();
			EditorGUILayout.EndHorizontal ();
			GUILayout.BeginHorizontal ();
			GUILayout.Space (categories.Count * 403);
			GUILayout.EndHorizontal ();
			for (int y = 0; y < categories.Count; y++) {
				for (int i = 0; i < categories.Count; i++) {
					if (categories [y].items.Count > categories [i].items.Count)
						GUILayout.Space (categories [y].items.Count * 7);
				}
				for (int x = 0; x < categories [y].items.Count; x++) {
                        
					if (categories [y].items [x].expanded)
						GUILayout.Space (190);
				}
			}

			//New list
			if (GUI.Button (new Rect ((position.width - 102) + scrollPosition.x, (position.height - 52) + scrollPosition.y, 82, 32), "Add new list")) {
				categories.Add (new ListCategories ());
				categories [categories.Count - 1].catName = "Category " + (categories.Count - 1).ToString ();
			}
		}
		if (!importTrelloView && rewardsView)
			EditorGUILayout.LabelField ("Points: " + points);
		if (!rewardsView && !importTrelloView) {
			EditorGUI.LabelField (new Rect ((position.width - 190) + scrollPosition.x, (position.height - 42) + scrollPosition.y, 140, 32), "Points: " + points);
		}
		GUILayout.EndScrollView ();
	}

	public void CategoryWindows (int i)
	{
		GUILayout.BeginHorizontal ();
		GUILayout.Space (5);
		//category names
		if (categories [i].editName) {
			if (Event.current.Equals (Event.KeyboardEvent ("return"))) {
				categories [i].catName = categories [i].catName;
				categories [i].editName = false;
			}
			categories [i].catName = GUILayout.TextArea (categories [i].catName, GUILayout.Width (255));
		} else
			GUILayout.Label (categories [i].catName, GUILayout.Width (253));

		//edit and finish editing buttons
		if (categories [i].editName) {
			if (GUILayout.Button ("Okay", GUILayout.Width (80))) {
				categories [i].catName = categories [i].catName;
				categories [i].editName = false;
			}
		} else if (GUILayout.Button ("Edit Name", GUILayout.Width (80))) {
			categories [i].catName = categories [i].catName;
			categories [i].editName = true;
			categories [i].catName = categories [i].catName;
		}

		//remove category
		if (GUILayout.Button ("-", GUILayout.Width (25))) {
			categories.Remove (categories [i]);
			return;
		}
		GUILayout.EndHorizontal ();
		GUILayout.Space (5);
		GUILayout.BeginVertical ();
		//every item
		for (int x = 0; x < categories [i].items.Count; x++) {
			GUILayout.BeginHorizontal ();
			GUILayout.Space (15);
			//toggle buttons
			GUI.color = categories [i].items [x].color;
			categories [i].items [x].completed = GUILayout.Toggle (categories [i].items [x].completed, "", GUILayout.Width (15));

			GUI.color = Color.white;
			//color
			categories [i].items [x].color = EditorGUILayout.ColorField (categories [i].items [x].color, GUILayout.Width (40));
			GUILayout.Space (-20);

			//add points
			if (categories [i].items [x].completed && !categories [i].items [x].redeemed) {
				categories [i].items [x].redeemed = true;
				points += categories [i].items [x].points;
			}
			//name of item and button to expand
			if (GUILayout.Button (categories [i].items [x].title, GUILayout.Width (120))) {
				if (categories [i].items [x].expanded)
					categories [i].items [x].expanded = false;
				else
					categories [i].items [x].expanded = true;
			}

			GUILayout.Label ("P: " + categories [i].items [x].points, GUILayout.Width (40));
			GUILayout.Label ("Due: " + categories [i].items [x].due, GUILayout.Width (100));

			//move up or down
			//cant move down
			if (x != categories [i].items.Count - 1) {
				if (GUILayout.Button ("V", GUILayout.Width (18))) {
					ListItem listItemX = categories [i].items [x + 1];
					categories [i].items [x + 1] = categories [i].items [x];
					categories [i].items [x] = listItemX;
				}
			}

			//cant move up
			if (x != 0) {
				if (GUILayout.Button ("Λ", GUILayout.Width (18))) {
					ListItem listItemX = categories [i].items [x - 1];
					categories [i].items [x - 1] = categories [i].items [x];
					categories [i].items [x] = listItemX;
				}
			}

			GUILayout.EndHorizontal ();
			GUILayout.Space (5);
			if (categories [i].items.Contains (categories [i].items [x]))
                //we are expanded show all the information
                if (categories [i].items [x].expanded) {
				GUILayout.BeginVertical ();
				//title
				GUILayout.BeginHorizontal ();
				GUILayout.Space (15);
				GUILayout.Label ("Title ", GUILayout.Width (80));
				categories [i].items [x].title = GUILayout.TextArea (categories [i].items [x].title, GUILayout.Width (183), GUILayout.Height (20));
				GUILayout.Label ("Points ", GUILayout.Width (40));
				categories [i].items [x].points = EditorGUILayout.IntField (categories [i].items [x].points, GUILayout.Width (40));
				GUILayout.EndHorizontal ();

				//Description
				GUILayout.BeginHorizontal ();
				GUILayout.Space (15);
				GUILayout.Label ("Description ", GUILayout.Width (80));
				categories [i].items [x].itemDescription = GUILayout.TextArea (categories [i].items [x].itemDescription, GUILayout.Width (270), GUILayout.Height (60));
				GUILayout.EndHorizontal ();

				//Date Assigned
				GUILayout.BeginHorizontal ();
				GUILayout.Space (15);
				GUILayout.Label ("Date Assigned: " + categories [i].items [x].assigned, GUILayout.Width (180));

				//Date Due
				GUILayout.Label ("Date Due: ", GUILayout.Width (65));
				categories [i].items [x].due = GUILayout.TextArea (categories [i].items [x].due, GUILayout.Width (100), GUILayout.Height (20));
				GUILayout.EndHorizontal ();

				//move
				GUILayout.BeginHorizontal ();
				GUILayout.Space (15);
				GUILayout.Label ("Move item to list: ", GUILayout.Width (110));
				int selectedList = i;
				List<string> optionsString = new List<string> ();
				for (int y = 0; y < categories.Count; y++) {
					//if(categories[i] != categories[y])
					optionsString.Add (categories [y].catName);
				}
				string[] optionsStringArray = optionsString.ToArray ();
				selectedList = EditorGUILayout.Popup (selectedList, optionsStringArray);
				for (int y = 0; y < categories.Count; y++) {
					if (y == selectedList && y != i) {
						categories [i].items [x].expanded = false;
						categories [y].items.Add (categories [i].items [x]);
						categories [i].items.Remove (categories [i].items [x]);
					}
				}
				GUILayout.EndHorizontal ();

				//remove
				GUILayout.BeginHorizontal ();
				GUILayout.Space (15);
				if (GUILayout.Button ("Delete Item")) {
					categories [i].items.Remove (categories [i].items [x]);
					return;
				}
				GUILayout.EndHorizontal ();

				GUILayout.Space (10);
				GUILayout.EndVertical ();
			}
		}

		GUILayout.BeginHorizontal ();
		//create new item
		if (GUILayout.Button ("Add new item")) {
			categories [i].items.Add (new ListItem ());
			DateTime dt = DateTime.Now;
			categories [i].items [categories [i].items.Count - 1].assigned = dt.Year.ToString () + "/" + dt.Month.ToString () + "/" + dt.Day.ToString ();
			categories [i].items [categories [i].items.Count - 1].title = "Item " + (categories [i].items.Count - 1).ToString ();
			categories [i].items [categories [i].items.Count - 1].itemDescription = "Description";
			categories [i].items [categories [i].items.Count - 1].due = "N/A";
			categories [i].items [categories [i].items.Count - 1].color = Color.white;
		}
		GUILayout.EndHorizontal ();

		GUILayout.Space (5);
		GUILayout.EndVertical ();
		GUILayout.Space (10);
	}

	void Import ()
	{
		importButtonText = "Retrieving Information...";
		Debug.Log ("[SNotes] Requesting information... Please wait");
		var www = new WWW (url + ".json");
		//wait for the www request
		ContinuationManager.Add (() => www.isDone, () => {
			if (www.text != "") {
				var N = JSON.Parse (www.text);
				if (N == null) {
					Debug.Log ("[SNotes] Board is not public");
					importButtonText = "Import"; 
					return;
				}
				int amountLists = N ["lists"].Count;
				for (int i = 0; i < amountLists; i++) {
					var idList = N ["lists"] [i] ["id"];
					ImportList (idList);
				}
				//buttonText = "Synced the board - " + nameMain;
			} else {
				Debug.Log ("[SNotes] Error - " + www.error);
				Debug.Log ("[SNotes] You might not be logged in, opening your browser...");
				Application.OpenURL ("https://trello.com/login");
				importButtonText = "Import";
			}
		});
	}

	void ImportList (string idList)
	{
		string url = "https://api.trello.com/1/lists/" + idList + "?fields=name&cards=open&card_fields=name&key=" + key;
		var www = new WWW (url);
		ContinuationManager.Add (() => www.isDone, () => {
			if (www.text != "") {
				var N = JSON.Parse (www.text);
				int amountCards = N ["cards"].Count;

				//LIST NAME

				for (int i = 0; i < amountCards; i++) {

					//CARD INFORMATION
					var nameList = N ["name"].Value;
                    
					var cardID = N ["cards"] [i] ["id"];
					ImportCard (cardID, nameList);
				}
			} else {
				Debug.Log ("[SNotes] Error - " + www.error);
				importButtonText = "Import";
			}
		});
	}

	void ImportCard (string idCard, string listName)
	{
		//fields options : desc, name, due
		string url = "https://api.trello.com/1/cards/" + idCard + "?fields=name,idList&member_fields=fullName&key=" + key;
		var wwwName = new WWW (url);

		//NAME
		ContinuationManager.Add (() => wwwName.isDone, () => {
			if (wwwName.text != "") {
				var N = JSON.Parse (wwwName.text);
				//Name variable
				var cardName = N ["name"].Value;

				//Description
				string urldesc = "https://api.trello.com/1/cards/" + idCard + "?fields=desc,idList&member_fields=fullName&key=" + key;
				var wwwDesc = new WWW (urldesc);
				ContinuationManager.Add (() => wwwDesc.isDone, () => {
					if (wwwDesc.text != "") {
						var O = JSON.Parse (wwwDesc.text);
						//Description Variable
						var cardDesc = O ["desc"].Value;

						//DUE DATE
						string urlDue = "https://api.trello.com/1/cards/" + idCard + "?fields=due,idList&member_fields=fullName&key=" + key;
						var wwwDue = new WWW (urlDue);
						ContinuationManager.Add (() => wwwDue.isDone, () => {
							if (wwwDue.text != "") {
								var P = JSON.Parse (wwwDue.text);
								//Due Date variable
								var cardDue = P ["due"].Value;

								//LABELS
								string urlLabel = "https://api.trello.com/1/cards/" + idCard + "?fields=labels,idList&member_fields=fullName&key=" + key;
								var wwwLabel = new WWW (urlLabel);
								ContinuationManager.Add (() => wwwLabel.isDone, () => {
									if (wwwLabel.text != "") {
										var Q = JSON.Parse (wwwLabel.text);
										//Label Color variable
										var cardLabel = Q ["labels"] [0] ["color"];

										//check if you should make a cat (category)
										bool makeCat = true;
										for (int i = 0; i < categories.Count; i++) {
											if (categories [i].catName == listName) {
												makeCat = false;
											}
										}
										//make the first one
										if (makeCat || categories.Count == 0) {
											categories.Add (new ListCategories ());
											curCategory = categories.Count - 1;
											categories [curCategory].catName = listName;
										}

										bool makeItem = true;
										for (int i = 0; i < categories [curCategory].items.Count; i++) {
											if (categories [curCategory].items [i].title == cardName) {
												makeItem = false;
											}
										}
										if (categories [curCategory].catName == listName) {
											//make the first one
											if (makeItem || categories [curCategory].items.Count == 0) {
												categories [curCategory].items.Add (new ListItem ());
												curItem = categories [curCategory].items.Count - 1;
											}
											//title
											categories [curCategory].items [curItem].title = cardName;
											//date
											DateTime dt = DateTime.Now;
											categories [curCategory].items [curItem].assigned = dt.Year.ToString () + "/" + dt.Month.ToString () + "/" + dt.Day.ToString ();
											//description
											categories [curCategory].items [curItem].itemDescription = cardDesc;
                                           
											//Format Due Date if it has a due date
											if (cardDue != "null") {
												var cardDueFormated = cardDue.Substring (0, 10);
												//set the date if its not null
												categories [curCategory].items [curItem].due = cardDueFormated;
											} else {
												//set the date if its null
												categories [curCategory].items [curItem].due = "N/A";
											}

											//color
											switch (cardLabel) {
											case "green":
												categories [curCategory].items [curItem].color = Color.green;
												break;
											case "yellow":
												categories [curCategory].items [curItem].color = Color.yellow;
												break;
											case "orange":
												categories [curCategory].items [curItem].color = new Color (1, 0.5f, 0);
												break;
											case "red":
												categories [curCategory].items [curItem].color = Color.red;
												break;
											case "purple":
												categories [curCategory].items [curItem].color = Color.magenta;
												break;
											case "blue":
												categories [curCategory].items [curItem].color = Color.blue;
												break;
											case "":
												categories [curCategory].items [curItem].color = Color.white;
												break;
											}
											if (cardLabel == null)
												categories [curCategory].items [curItem].color = Color.white;
										}
										importButtonText = "Import";
										Debug.Log ("[SNotes] Completed Import");
									} else {
										Debug.Log ("[SNotes] Error - " + wwwDue.error);
										importButtonText = "Import";
									}
								});
							} else {
								Debug.Log ("[SNotes] Error - " + wwwDue.error);
								importButtonText = "Import";
							}
						});
					} else {
						Debug.Log ("[SNotes] Error - " + wwwDesc.error);
						importButtonText = "Import";
					}
				});
			} else {
				Debug.Log ("[SNotes] Error - " + wwwName.error);
				importButtonText = "Import";
			}
		});
	}
}
#endif