using UnityEngine;

namespace ElectrumGames.Extensions.UI
{
	public class SafeArea : MonoBehaviour
	{
		public enum SimDevice { None, iPhoneX }
		public static SimDevice Sim = SimDevice.None;
		Rect[] NSA_iPhoneX = new Rect[]
		{
			new Rect (0f, 102f / 2436f, 1f, 2202f / 2436f),  // Portrait
			new Rect (132f / 2436f, 63f / 1125f, 2172f / 2436f, 1062f / 1125f)  // Landscape
		};
		RectTransform Panel;
		Rect LastSafeArea = new Rect(0, 0, 0, 0);
		Vector2 defaultAnchorMin;
		Vector2 defaultAnchorMax;

		private bool isLogsAllowed = false;

		void Awake()
		{
			isLogsAllowed = Debug.isDebugBuild;

			Panel = GetComponent<RectTransform>();
			defaultAnchorMin = Panel.anchorMin;
			defaultAnchorMax = Panel.anchorMax;

			if (isLogsAllowed)
				Debug.Log($"{this} Set screen safe zone. screenSafe={Screen.safeArea}");
		}

		private void OnEnable()
		{
			Refresh();
		}

		private void OnDisable()
		{
			ResetSafeArea();
		}

		[ContextMenu("Refresh")]
		private void Refresh()
		{
			Rect safeArea = GetSafeArea();
			if (safeArea != LastSafeArea)
				ApplySafeArea(safeArea);
		}

		Rect GetSafeArea()
		{
			Rect safeArea = Screen.safeArea;

			if (Application.isEditor && Sim != SimDevice.None)
			{
				var screenWidth = ScreenWidth;
				var screenHeight = ScreenHeight;
				Rect nsa = new Rect(0, 0, screenWidth, screenHeight);

				switch (Sim)
				{
					case SimDevice.iPhoneX:
						if (screenHeight > screenWidth)  // Portrait
							nsa = NSA_iPhoneX[0];
						else  // Landscape
							nsa = NSA_iPhoneX[1];
						break;
					default:
						break;
				}

				safeArea = new Rect(screenWidth * nsa.x, screenHeight * nsa.y, screenWidth * nsa.width, screenHeight * nsa.height);
				if (isLogsAllowed)
					Debug.Log($"{this} editor fixed safe area={safeArea}");

			}

			return safeArea;
		}

		void ApplySafeArea(Rect r)
		{
			LastSafeArea = r;

			// Convert safe area rectangle from absolute pixels to normalised anchor coordinates
			Vector2 anchorMin = r.position;
			Vector2 anchorMax = r.position + r.size;
			var screenWidth = ScreenWidth;
			var screenHeight = ScreenHeight;
			anchorMin.x /= screenWidth;
			anchorMin.y /= screenHeight;
			anchorMax.x /= screenWidth;
			anchorMax.y /= screenHeight;
			Panel.anchorMin = anchorMin;
			Panel.anchorMax = anchorMax;

			if (isLogsAllowed)
				Debug.Log($"{this} apply safe area : anchors={Panel.anchorMin}::{Panel.anchorMax} rect={r} screen={screenWidth},{screenHeight}");
		}

		void ResetSafeArea()
		{
			Panel.anchorMin = defaultAnchorMin;
			Panel.anchorMax = defaultAnchorMax;
			LastSafeArea = new Rect();
		}

		int ScreenWidth => Application.isEditor ? Screen.width : Screen.currentResolution.width;
		int ScreenHeight => Application.isEditor ? Screen.height : Screen.currentResolution.height;

		public void SetSimDevice(SimDevice simDevice)
		{
			Sim = simDevice;
			Refresh();
		}

		public override string ToString()
		{
			return $"[{GetType().Name}]";
		}

	}
}