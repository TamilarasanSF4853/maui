#if ANDROID || IOS
using NUnit.Framework;
using UITest.Appium.NUnit;
using UITest.Appium;
using UITest.Core;
using System.ComponentModel;

namespace Microsoft.Maui.TestCases.Tests
{
	[Category(UITestCategories.SafeAreaEdges)]
	public class SafeArea_BorderFeatureTests : _GalleryUITest
	{
		public const string SafeAreaFeatureMatrix = "SafeArea Feature Matrix";
		public override string GalleryPageName => SafeAreaFeatureMatrix;

		public SafeArea_BorderFeatureTests(TestDevice device)
			: base(device)
		{
		}

		/// <summary>
		/// Reads and parses safe area inset values from the SafeAreaInsetsLabel.
		/// Format: "L:{left},T:{top},R:{right},B:{bottom},KH:{keyboardHeight},CoL:{cutoutLeft},CoR:{cutoutRight}"
		/// </summary>
		private (int Left, int Top, int Right, int Bottom, int KeyboardHeight, int CutoutL, int CutoutR) GetSafeAreaInsets()
		{
			var text = App.WaitForElement("SafeAreaInsetsLabel").GetText() ?? string.Empty;
			var match = System.Text.RegularExpressions.Regex.Match(text, @"L:(\d+),T:(\d+),R:(\d+),B:(\d+),KH:(\d+),CoL:(\d+),CoR:(\d+)");
			if (!match.Success)
				throw new InvalidOperationException($"Failed to parse safe area insets from: '{text}'");
			return (
				int.Parse(match.Groups[1].Value),
				int.Parse(match.Groups[2].Value),
				int.Parse(match.Groups[3].Value),
				int.Parse(match.Groups[4].Value),
				int.Parse(match.Groups[5].Value),
				int.Parse(match.Groups[6].Value),
				int.Parse(match.Groups[7].Value)
			);
		}

		private int GetKeyboardY()
		{
#if IOS
			if (App is AppiumIOSApp iosApp && HelperExtensions.IsIOS26OrHigher(iosApp))
			{
				var rect = App.WaitForElement("Toolbar").GetRect();
				return rect.Y;
			}
			else
			{
				var rect = App.WaitForElement("Done").GetRect();
				return rect.Y;
			}
#elif ANDROID
			var (_, screenHeight) = GetScreenSize();
			var insets = GetSafeAreaInsets();
			return screenHeight - insets.KeyboardHeight;
#endif
		}

		/// <summary>
		/// Navigates to SafeAreaBorderPage from the feature matrix main page.
		/// If already on SafeAreaBorderPage (button not visible), skips navigation.
		/// </summary>
		public void ClickBorderSafeAreaButton()
		{
			var isButtonPresent = App.FindElement("BorderSafeAreaButton");
			if (isButtonPresent != null)
			{
				App.WaitForElement("BorderSafeAreaButton");
				App.Tap("BorderSafeAreaButton");
			}
		}

		private (int Width, int Height) GetScreenSize()
		{
			var size = ((AppiumApp)App).Driver.Manage().Window.Size;
			return (size.Width, size.Height);
		}

		private int GetLandscapeRightInset(int right, int cutoutR)
		{
#if ANDROID
			return cutoutR;
#else
			return right;
#endif
		}

		// ──────────────────────────────────────────────
		// Uniform SafeAreaRegions via Buttons
		// ──────────────────────────────────────────────

		[Test, Order(1)]
		[Description("Border content extends edge-to-edge behind system bars/notch")]
		public void ValidateSafeAreaEdges_None()
		{
			ClickBorderSafeAreaButton();

			App.WaitForElement("SafeAreaNoneButton");
			App.Tap("SafeAreaNoneButton");
			Assert.That(App.FindElement("SafeAreaEdgesValueLabel").GetText(), Is.EqualTo("None"));

			// Portrait: top label Y should be ≈ 0 (edge-to-edge, Border applies no safe area padding)
			var topLabelRect = App.WaitForElement("TopEdgeIndicator").GetRect();
			Assert.That(topLabelRect.Y, Is.EqualTo(0),
				$"None: top label Y ({topLabelRect.Y}) should be = 0 (edge-to-edge), safe area top inset is ignored");

			var (_, screenHeight) = GetScreenSize();

			// Portrait: bottom label bottom edge should be ≈ screenHeight (edge-to-edge, no safe area applied)
			var bottomLabelRect = App.WaitForElement("BottomEdgeIndicator").GetRect();
			Assert.That(Math.Abs(bottomLabelRect.Bottom), Is.EqualTo(screenHeight),
				$"None: bottom label Y ({bottomLabelRect.Bottom}) should be ≈ screenHeight ({screenHeight})");
		}

		[Test, Order(2)]
		[Description("Border content inset from all system UI (status bar, nav bar, notch, home indicator)")]
		public void ValidateSafeAreaEdges_All()
		{
			ClickBorderSafeAreaButton();

			App.Tap("SafeAreaAllButton");
			Assert.That(App.FindElement("SafeAreaEdgesValueLabel").GetText(), Is.EqualTo("All"));

			var insets = GetSafeAreaInsets();
			var (_, screenHeight) = GetScreenSize();

			// Portrait: top label Y should be ≈ insets.Top (Border padding absorbs safe area top)
			var topLabelRect = App.WaitForElement("TopEdgeIndicator").GetRect();
			Assert.That(Math.Abs(topLabelRect.Y), Is.EqualTo(insets.Top),
				$"All: top label Y ({topLabelRect.Y}) should be equal to insets.Top ({insets.Top})");

			// Portrait: bottom label bottom edge should be ≈ screenBottom - insets.Bottom
			var bottomLabelRect = App.WaitForElement("BottomEdgeIndicator").GetRect();
			Assert.That(Math.Abs(bottomLabelRect.Bottom), Is.EqualTo(screenHeight - insets.Bottom),
				$"All: bottom label Y ({bottomLabelRect.Bottom}) should be equal to (screenHeight - insets.Bottom) ({screenHeight - insets.Bottom})");
		}

		[Test, Order(3)]
		[Description("Border content avoids system bars/notch but can extend under keyboard area")]
		public void ValidateSafeAreaEdges_Container()
		{
			ClickBorderSafeAreaButton();

			App.Tap("SafeAreaContainerButton");
			Assert.That(App.FindElement("SafeAreaEdgesValueLabel").GetText(), Is.EqualTo("Container"));

			var insets = GetSafeAreaInsets();
			var (_, screenHeight) = GetScreenSize();

			// Portrait: top label Y should be ≈ insets.Top (Border padding absorbs safe area top)
			var topLabelRect = App.WaitForElement("TopEdgeIndicator").GetRect();
			Assert.That(Math.Abs(topLabelRect.Y), Is.EqualTo(insets.Top),
				$"Container: top label Y ({topLabelRect.Y}) should be equal to insets.Top ({insets.Top})");

			// Portrait: bottom label bottom edge should be ≈ screenBottom - insets.Bottom
			var bottomLabelRect = App.WaitForElement("BottomEdgeIndicator").GetRect();
			Assert.That(Math.Abs(bottomLabelRect.Bottom), Is.EqualTo(screenHeight - insets.Bottom),
				$"Container: bottom label Y ({bottomLabelRect.Bottom}) should be equal to (screenHeight - insets.Bottom) ({screenHeight - insets.Bottom})");
		}

		[Test, Order(4)]
		[Description("Border SoftInput respects safe area on top/sides but bottom is edge-to-edge without keyboard")]
		public void ValidateSafeAreaEdges_SoftInput()
		{
			ClickBorderSafeAreaButton();

			App.WaitForElement("SafeAreaSoftInputButton");
			App.Tap("SafeAreaSoftInputButton");

			Assert.That(App.FindElement("SafeAreaEdgesValueLabel").GetText(), Is.EqualTo("SoftInput"));

			var insets = GetSafeAreaInsets();
			var (_, screenHeight) = GetScreenSize();

			// Portrait: top label Y should be ≈ insets.Top (SoftInput respects notch/safe area)
			var topLabelRect = App.WaitForElement("TopEdgeIndicator").GetRect();
			Assert.That(Math.Abs(topLabelRect.Y), Is.EqualTo(insets.Top),
				$"SoftInput: top label Y ({topLabelRect.Y}) should be equal to insets.Top ({insets.Top})");

			// Portrait: bottom label bottom edge should be ≈ screenHeight (edge-to-edge, no safe area applied)
			var bottomLabelRect = App.WaitForElement("BottomEdgeIndicator").GetRect();
			Assert.That(Math.Abs(bottomLabelRect.Bottom), Is.EqualTo(screenHeight),
				$"SoftInput: bottom label Y ({bottomLabelRect.Bottom}) should be equal to screenHeight ({screenHeight})");
		}

#if TEST_FAILS_ON_ANDROID && TEST_FAILS_ON_IOS // Issue Link - https://github.com/dotnet/maui/issues/34872
		[Test, Order(5)]
		[Description("Default on Border behaves as None — content extends edge-to-edge")]
		public void ValidateSafeAreaEdges_Default()
		{
			ClickBorderSafeAreaButton();

			App.WaitForElement("SafeAreaDefaultButton");
			App.Tap("SafeAreaDefaultButton");

			Assert.That(App.FindElement("SafeAreaEdgesValueLabel").GetText(), Is.EqualTo("Default"));

			var (_, screenHeight) = GetScreenSize();

			// Portrait: top label Y should be 0 (edge-to-edge, Default on Border = None)
			var topLabelRect = App.WaitForElement("TopEdgeIndicator").GetRect();
			Assert.That(topLabelRect.Y, Is.EqualTo(0),
				$"Default: top label Y ({topLabelRect.Y}) should be = 0 (edge-to-edge, Default on Border = None)");

			// Portrait: bottom label bottom edge should be ≈ screenHeight (edge-to-edge)
			var bottomLabelRect = App.WaitForElement("BottomEdgeIndicator").GetRect();
			Assert.That(Math.Abs(bottomLabelRect.Bottom), Is.EqualTo(screenHeight),
				$"Default: bottom label Y ({bottomLabelRect.Bottom}) should be ≈ screenHeight ({screenHeight})");
		}
#endif

		// ──────────────────────────────────────────────
		// Per-Edge Configuration (via Options)
		// ──────────────────────────────────────────────

		[Test, Order(6)]
		[Description("Only top avoids status bar/notch. Bottom edge-to-edge.")]
		public void ValidatePerEdge_TopContainerOnly()
		{
			ClickBorderSafeAreaButton();

			App.WaitForElement("Options");
			App.Tap("Options");
			App.WaitForElement("TopContainer");
			App.Tap("TopContainer");
			App.Tap("BottomNone");
			App.WaitForElement("Apply");
			App.Tap("Apply");

			App.WaitForElement("SafeAreaEdgesValueLabel");
			Assert.That(App.FindElement("SafeAreaEdgesValueLabel").GetText(), Is.EqualTo("L:None, T:Container, R:None, B:None"));

			var insets = GetSafeAreaInsets();
			var (_, screenHeight) = GetScreenSize();

			// Portrait: Container — Border top padding absorbs safe area
			var topLabelRect = App.WaitForElement("TopEdgeIndicator").GetRect();
			Assert.That(Math.Abs(topLabelRect.Y), Is.EqualTo(insets.Top),
				$"Top (Container): label Y ({topLabelRect.Y}) should be ≈ insets.Top ({insets.Top})");

			// Portrait: bottom label bottom edge should be ≈ screenHeight (edge-to-edge, no safe area applied)
			var bottomLabelRect = App.WaitForElement("BottomEdgeIndicator").GetRect();
			Assert.That(Math.Abs(bottomLabelRect.Bottom), Is.EqualTo(screenHeight),
				$"None: bottom label Y ({bottomLabelRect.Bottom}) should be ≈ screenHeight ({screenHeight})");
		}

		[Test, Order(7)]
		[Description("Top avoids system bars; bottom avoids only keyboard")]
		public void ValidatePerEdge_BottomSoftInput_TopContainer()
		{
			ClickBorderSafeAreaButton();

			App.WaitForElement("Options");
			App.Tap("Options");
			App.WaitForElement("TopContainer");
			App.Tap("TopContainer");
			App.Tap("BottomSoftInput");
			App.WaitForElement("Apply");
			App.Tap("Apply");

			App.WaitForElement("SafeAreaEdgesValueLabel");
			Assert.That(App.FindElement("SafeAreaEdgesValueLabel").GetText(), Is.EqualTo("L:None, T:Container, R:None, B:SoftInput"));

			var insets = GetSafeAreaInsets();
			var (_, screenHeight) = GetScreenSize();

			// Portrait: only validate top and bottom — no left/right safe area insets in portrait
			// Top: Container — Border top padding absorbs safe area
			var topLabelRect = App.WaitForElement("TopEdgeIndicator").GetRect();
			Assert.That(Math.Abs(topLabelRect.Y), Is.EqualTo(insets.Top),
				$"Top (Container): label Y ({topLabelRect.Y}) should be ≈ insets.Top ({insets.Top})");

			// Portrait: bottom label bottom edge should be ≈ screenHeight (edge-to-edge, SoftInput without keyboard)
			var bottomLabelRect = App.WaitForElement("BottomEdgeIndicator").GetRect();
			Assert.That(Math.Abs(bottomLabelRect.Bottom), Is.EqualTo(screenHeight),
				$"SoftInput: bottom label Y ({bottomLabelRect.Bottom}) should be ≈ screenHeight ({screenHeight})");
		}

		[Test, Order(8)]
		[Description("Top/bottom respect all insets")]
		public void ValidatePerEdge_TopBottomAll_SidesNone()
		{
			ClickBorderSafeAreaButton();

			App.WaitForElement("Options");
			App.Tap("Options");
			App.WaitForElement("TopAll");
			App.Tap("TopAll");
			App.Tap("BottomAll");
			App.WaitForElement("Apply");
			App.Tap("Apply");

			App.WaitForElement("SafeAreaEdgesValueLabel");
			Assert.That(App.FindElement("SafeAreaEdgesValueLabel").GetText(), Is.EqualTo("L:None, T:All, R:None, B:All"));

			var insets = GetSafeAreaInsets();
			var (_, screenHeight) = GetScreenSize();

			// Portrait: top label Y should be ≈ insets.Top (All applies safe area on top)
			var topLabelRect = App.WaitForElement("TopEdgeIndicator").GetRect();
			Assert.That(Math.Abs(topLabelRect.Y), Is.EqualTo(insets.Top),
				$"All: top label Y ({topLabelRect.Y}) should be = insets.Top ({insets.Top})");

			// Portrait: bottom label bottom edge should be ≈ screenBottom - insets.Bottom
			var bottomLabelRect = App.WaitForElement("BottomEdgeIndicator").GetRect();
			Assert.That(Math.Abs(bottomLabelRect.Bottom), Is.EqualTo(screenHeight - insets.Bottom),
				$"All: bottom label Y ({bottomLabelRect.Bottom}) should be = (screenHeight - insets.Bottom) ({screenHeight - insets.Bottom})");
		}

		[Test, Order(9)]
		[Description("Each edge independently applies its behavior")]
		public void ValidatePerEdge_AllDifferent()
		{
			ClickBorderSafeAreaButton();

			App.WaitForElement("Options");
			App.Tap("Options");
			App.WaitForElement("TopContainer");
			App.Tap("TopContainer");
			App.Tap("BottomAll");
			App.WaitForElement("Apply");
			App.Tap("Apply");

			App.WaitForElement("SafeAreaEdgesValueLabel");
			Assert.That(App.FindElement("SafeAreaEdgesValueLabel").GetText(), Is.EqualTo("L:None, T:Container, R:None, B:All"));

			var insets = GetSafeAreaInsets();
			var (_, screenHeight) = GetScreenSize();

			// Portrait: top label Y should be ≈ insets.Top (safe area applied)
			var topLabelRect = App.WaitForElement("TopEdgeIndicator").GetRect();
			Assert.That(Math.Abs(topLabelRect.Y), Is.EqualTo(insets.Top),
				$"Container: top label Y ({topLabelRect.Y}) should be equal to insets.Top ({insets.Top})");

			// Portrait: bottom label bottom edge should be ≈ screenBottom - insets.Bottom
			var bottomLabelRect = App.WaitForElement("BottomEdgeIndicator").GetRect();
			Assert.That(Math.Abs(bottomLabelRect.Bottom), Is.EqualTo(screenHeight - insets.Bottom),
				$"All: bottom label Y ({bottomLabelRect.Bottom}) should be equal to (screenHeight - insets.Bottom) ({screenHeight - insets.Bottom})");
		}

		// ──────────────────────────────────────────────
		// Keyboard Interaction (SoftInput)
		// ──────────────────────────────────────────────

#if TEST_FAILS_ON_IOS // Issue Link - https://github.com/dotnet/maui/issues/34846

		[Test, Order(10)]
		[Description("None → All → keyboard open → Container → dismiss → All: positions correct at each step")]
		public void ValidateSafeArea_NoneThenAllKeyboardContainerDismissAll()
		{
			ClickBorderSafeAreaButton();
			App.DismissKeyboard();

			var insets = GetSafeAreaInsets();
			var (_, screenHeight) = GetScreenSize();

			// ── Step 1: Click None and verify ──
			App.WaitForElement("SafeAreaNoneButton");
			App.Tap("SafeAreaNoneButton");
			Assert.That(App.FindElement("SafeAreaEdgesValueLabel").GetText(), Is.EqualTo("None"));

			var topLabelRect = App.WaitForElement("TopEdgeIndicator").GetRect();
			Assert.That(topLabelRect.Y, Is.EqualTo(0),
				$"None: top label Y ({topLabelRect.Y}) should be 0 (edge-to-edge)");

			var bottomLabelRect = App.WaitForElement("BottomEdgeIndicator").GetRect();
			Assert.That(Math.Abs(bottomLabelRect.Bottom), Is.EqualTo(screenHeight),
				$"None: bottom label Bottom ({bottomLabelRect.Bottom}) should be equal to screenHeight ({screenHeight})");

			// ── Step 2: Click All and verify ──
			App.Tap("SafeAreaAllButton");
			Assert.That(App.FindElement("SafeAreaEdgesValueLabel").GetText(), Is.EqualTo("All"));

			topLabelRect = App.WaitForElement("TopEdgeIndicator").GetRect();
			Assert.That(Math.Abs(topLabelRect.Y), Is.EqualTo(insets.Top),
				$"All: top label Y ({topLabelRect.Y}) should be equal to insets.Top ({insets.Top})");

			bottomLabelRect = App.WaitForElement("BottomEdgeIndicator").GetRect();
			Assert.That(Math.Abs(bottomLabelRect.Bottom), Is.EqualTo(screenHeight - insets.Bottom),
				$"All: bottom label Bottom ({bottomLabelRect.Bottom}) should be equal to (screenHeight - insets.Bottom) ({screenHeight - insets.Bottom})");

			// ── Step 3: Open keyboard and verify (All adjusts for keyboard) ──
			Assert.That(App.IsKeyboardShown(), Is.False, "Keyboard should not be visible before tapping entry");
			App.Tap("SafeAreaTestEntry");
			App.WaitForKeyboardToShow();
			Assert.That(App.IsKeyboardShown(), Is.True, "Keyboard should be visible after tapping entry");

			var keyboardY = GetKeyboardY();

			topLabelRect = App.WaitForElement("TopEdgeIndicator").GetRect();
			Assert.That(Math.Abs(topLabelRect.Y), Is.EqualTo(insets.Top),
				$"All (keyboard open): top label Y ({topLabelRect.Y}) should be equal to insets.Top ({insets.Top})");

			bottomLabelRect = App.WaitForElement("BottomEdgeIndicator").GetRect();
			Assert.That(bottomLabelRect.Bottom, Is.EqualTo(keyboardY),
				$"All (keyboard open): bottom label Bottom ({bottomLabelRect.Bottom}) should equal keyboard Y ({keyboardY})");

			// ── Step 4: Switch to Container while keyboard is open ──
			App.Tap("SafeAreaContainerButton");
			Assert.That(App.FindElement("SafeAreaEdgesValueLabel").GetText(), Is.EqualTo("Container"));

			topLabelRect = App.WaitForElement("TopEdgeIndicator").GetRect();
			Assert.That(Math.Abs(topLabelRect.Y), Is.EqualTo(insets.Top),
				$"Container (keyboard open): top label Y ({topLabelRect.Y}) should be equal to insets.Top ({insets.Top})");

#if !ANDROID // On Android, Appium does not find the bottom label when the keyboard is open
			bottomLabelRect = App.WaitForElement("BottomEdgeIndicator").GetRect();
			Assert.That(Math.Abs(bottomLabelRect.Bottom), Is.EqualTo(screenHeight - insets.Bottom),
				$"Container (keyboard open): bottom label Bottom ({bottomLabelRect.Bottom}) should be equal to (screenHeight - insets.Bottom) ({screenHeight - insets.Bottom})");
#endif
			// ── Step 5: Dismiss keyboard ──
			App.DismissKeyboard();
			App.WaitForKeyboardToHide();
			Assert.That(App.IsKeyboardShown(), Is.False, "Keyboard should be hidden after dismissal");

			// ── Step 6: Click All and verify ──
			App.Tap("SafeAreaAllButton");
			Assert.That(App.FindElement("SafeAreaEdgesValueLabel").GetText(), Is.EqualTo("All"));

			topLabelRect = App.WaitForElement("TopEdgeIndicator").GetRect();
			Assert.That(Math.Abs(topLabelRect.Y), Is.EqualTo(insets.Top),
				$"All (after dismiss): top label Y ({topLabelRect.Y}) should be equal to insets.Top ({insets.Top})");

			bottomLabelRect = App.WaitForElement("BottomEdgeIndicator").GetRect();
			Assert.That(Math.Abs(bottomLabelRect.Bottom), Is.EqualTo(screenHeight - insets.Bottom),
				$"All (after dismiss): bottom label Bottom ({bottomLabelRect.Bottom}) should be equal to (screenHeight - insets.Bottom) ({screenHeight - insets.Bottom})");
		}
#endif

		// ──────────────────────────────────────────────
		// Keyboard Position Validation
		// ──────────────────────────────────────────────
		// Validates that the bottom indicator moves up when keyboard is shown with modes that
		// adjust for keyboard (All/SoftInput), and does NOT move with modes that don't (None/Container).

		[Test, Order(11)]
		[Description("With All, bottom indicator moves up when keyboard is shown and restores when dismissed")]
		public void ValidateKeyboard_All_BottomMovesUp()
		{
			ClickBorderSafeAreaButton();
			App.DismissKeyboard();

			App.WaitForElement("SafeAreaAllButton");
			App.Tap("SafeAreaAllButton");

			Assert.That(App.FindElement("SafeAreaEdgesValueLabel").GetText(), Is.EqualTo("All"));

			var insets = GetSafeAreaInsets();
			var (_, screenHeight) = GetScreenSize();

			// ── Before keyboard ──
			var topLabelBeforeRect = App.WaitForElement("TopEdgeIndicator").GetRect();
			Assert.That(Math.Abs(topLabelBeforeRect.Y), Is.EqualTo(insets.Top),
				$"Before keyboard - top label Y ({topLabelBeforeRect.Y}) should be equal to insets.Top ({insets.Top})");

			var bottomLabelBeforeRect = App.WaitForElement("BottomEdgeIndicator").GetRect();
			Assert.That(Math.Abs(bottomLabelBeforeRect.Bottom), Is.EqualTo(screenHeight - insets.Bottom),
				$"Before keyboard - bottom label Bottom ({bottomLabelBeforeRect.Bottom}) should be equal to (screenHeight - insets.Bottom) ({screenHeight - insets.Bottom})");

			// ── Show keyboard ──
			Assert.That(App.IsKeyboardShown(), Is.False, "Keyboard should not be visible before tapping entry");
			App.Tap("SafeAreaTestEntry");
			App.WaitForKeyboardToShow();
			Assert.That(App.IsKeyboardShown(), Is.True, "Keyboard should be visible after tapping entry");

			var keyboardY = GetKeyboardY();

			// Bottom should have moved up to the keyboard top
			var bottomLabelDuringRect = App.WaitForElement("BottomEdgeIndicator").GetRect();
			Assert.That(bottomLabelDuringRect.Bottom, Is.EqualTo(keyboardY),
				$"During keyboard - bottom label Bottom ({bottomLabelDuringRect.Bottom}) should equal keyboard Y ({keyboardY})");

			// Top should remain unchanged
			var topLabelDuringRect = App.WaitForElement("TopEdgeIndicator").GetRect();
			Assert.That(topLabelDuringRect.Y, Is.EqualTo(topLabelBeforeRect.Y),
				$"During keyboard - top label Y ({topLabelDuringRect.Y}) should remain at ({topLabelBeforeRect.Y})");

			// ── Dismiss keyboard ──
			App.DismissKeyboard();
			App.WaitForKeyboardToHide();
			Assert.That(App.IsKeyboardShown(), Is.False, "Keyboard should be hidden after dismissal");

			// Top should return to its original position
			var topLabelAfterRect = App.WaitForElement("TopEdgeIndicator").GetRect();
			Assert.That(topLabelAfterRect.Y, Is.EqualTo(topLabelBeforeRect.Y),
				$"After keyboard - top label Y ({topLabelAfterRect.Y}) should return to original ({topLabelBeforeRect.Y})");

			// Bottom should return to its original position
			var bottomLabelAfterRect = App.WaitForElement("BottomEdgeIndicator").GetRect();
			Assert.That(bottomLabelAfterRect.Bottom, Is.EqualTo(bottomLabelBeforeRect.Bottom),
				$"After keyboard - bottom label Bottom ({bottomLabelAfterRect.Bottom}) should return to original ({bottomLabelBeforeRect.Bottom})");
		}

		[Test, Order(12)]
		[Description("With SoftInput, bottom indicator moves up when keyboard is shown and restores when dismissed")]
		public void ValidateKeyboard_SoftInput_BottomMovesUp()
		{
			ClickBorderSafeAreaButton();
			App.DismissKeyboard();

			App.WaitForElement("SafeAreaSoftInputButton");
			App.Tap("SafeAreaSoftInputButton");

			Assert.That(App.FindElement("SafeAreaEdgesValueLabel").GetText(), Is.EqualTo("SoftInput"));

			var insets = GetSafeAreaInsets();
			var (_, screenHeight) = GetScreenSize();

			// ── Before keyboard ──
			var topLabelBeforeRect = App.WaitForElement("TopEdgeIndicator").GetRect();
			Assert.That(Math.Abs(topLabelBeforeRect.Y), Is.EqualTo(insets.Top),
				$"Before keyboard - top label Y ({topLabelBeforeRect.Y}) should be equal to insets.Top ({insets.Top})");

			var bottomLabelBeforeRect = App.WaitForElement("BottomEdgeIndicator").GetRect();
			Assert.That(Math.Abs(bottomLabelBeforeRect.Bottom), Is.EqualTo(screenHeight),
				$"Before keyboard - bottom label Bottom ({bottomLabelBeforeRect.Bottom}) should be equal to screenHeight ({screenHeight})");

			// ── Show keyboard ──
			Assert.That(App.IsKeyboardShown(), Is.False, "Keyboard should not be visible before tapping entry");
			App.Tap("SafeAreaTestEntry");
			App.WaitForKeyboardToShow();
			Assert.That(App.IsKeyboardShown(), Is.True, "Keyboard should be visible after tapping entry");

			var keyboardY = GetKeyboardY();

			// Bottom should have moved up to the keyboard top
			var bottomLabelDuringRect = App.WaitForElement("BottomEdgeIndicator").GetRect();
			Assert.That(bottomLabelDuringRect.Bottom, Is.EqualTo(keyboardY),
				$"During keyboard - bottom label Bottom ({bottomLabelDuringRect.Bottom}) should equal keyboard Y ({keyboardY})");

			// Top should remain unchanged
			var topLabelDuringRect = App.WaitForElement("TopEdgeIndicator").GetRect();
			Assert.That(topLabelDuringRect.Y, Is.EqualTo(topLabelBeforeRect.Y),
				$"During keyboard - top label Y ({topLabelDuringRect.Y}) should remain at ({topLabelBeforeRect.Y})");

			// ── Dismiss keyboard ──
			App.DismissKeyboard();
			App.WaitForKeyboardToHide();
			Assert.That(App.IsKeyboardShown(), Is.False, "Keyboard should be hidden after dismissal");

			// Top should return to its original position
			var topLabelAfterRect = App.WaitForElement("TopEdgeIndicator").GetRect();
			Assert.That(topLabelAfterRect.Y, Is.EqualTo(topLabelBeforeRect.Y),
				$"After keyboard - top label Y ({topLabelAfterRect.Y}) should return to original ({topLabelBeforeRect.Y})");

			// Bottom should return to its original position
			var bottomLabelAfterRect = App.WaitForElement("BottomEdgeIndicator").GetRect();
			Assert.That(bottomLabelAfterRect.Bottom, Is.EqualTo(bottomLabelBeforeRect.Bottom),
				$"After keyboard - bottom label Bottom ({bottomLabelAfterRect.Bottom}) should return to original ({bottomLabelBeforeRect.Bottom})");
		}

		[Test, Order(13)]
		[Description("With None, bottom indicator does NOT move when keyboard is shown")]
		public void ValidateKeyboard_None_BottomStays()
		{
			ClickBorderSafeAreaButton();
			App.DismissKeyboard();

			App.WaitForElement("SafeAreaNoneButton");
			App.Tap("SafeAreaNoneButton");

			var (_, screenHeight) = GetScreenSize();

			// ── Before keyboard ──
			var topLabelBeforeRect = App.WaitForElement("TopEdgeIndicator").GetRect();
			Assert.That(topLabelBeforeRect.Y, Is.EqualTo(0),
				$"Before keyboard - top label Y ({topLabelBeforeRect.Y}) should be 0 (edge-to-edge)");

			var bottomLabelBeforeRect = App.WaitForElement("BottomEdgeIndicator").GetRect();
			Assert.That(Math.Abs(bottomLabelBeforeRect.Bottom), Is.EqualTo(screenHeight),
				$"Before keyboard - bottom label Bottom ({bottomLabelBeforeRect.Bottom}) should be equal to screenHeight ({screenHeight})");

			// ── Show keyboard ──
			Assert.That(App.IsKeyboardShown(), Is.False, "Keyboard should not be visible before tapping entry");
			App.Tap("SafeAreaTestEntry");
			App.WaitForKeyboardToShow();
			Assert.That(App.IsKeyboardShown(), Is.True, "Keyboard should be visible after tapping entry");

			var topLabelDuringRect = App.WaitForElement("TopEdgeIndicator").GetRect();
			Assert.That(topLabelDuringRect.Y, Is.EqualTo(0),
				$"During keyboard - top label Y ({topLabelDuringRect.Y}) should be 0 (edge-to-edge)");

#if !ANDROID // On Android, Appium does not find the bottom label when the keyboard is open
			var bottomLabelDuringRect = App.WaitForElement("BottomEdgeIndicator").GetRect();
			Assert.That(Math.Abs(bottomLabelDuringRect.Bottom), Is.EqualTo(screenHeight),
				$"During keyboard - bottom label Bottom ({bottomLabelDuringRect.Bottom}) should be equal to screenHeight ({screenHeight})");
#endif
			App.DismissKeyboard();
			App.WaitForKeyboardToHide();
			Assert.That(App.IsKeyboardShown(), Is.False, "Keyboard should be hidden after dismissal");

			var topLabelAfterRect = App.WaitForElement("TopEdgeIndicator").GetRect();
			Assert.That(topLabelAfterRect.Y, Is.EqualTo(topLabelBeforeRect.Y),
				$"After keyboard - top label Y ({topLabelAfterRect.Y}) should return to original ({topLabelBeforeRect.Y})");

			var bottomLabelAfterRect = App.WaitForElement("BottomEdgeIndicator").GetRect();
			Assert.That(bottomLabelAfterRect.Bottom, Is.EqualTo(bottomLabelBeforeRect.Bottom),
				$"After keyboard - bottom label Bottom ({bottomLabelAfterRect.Bottom}) should return to original ({bottomLabelBeforeRect.Bottom})");
		}

		[Test, Order(14)]
		[Description("With Container, bottom indicator does NOT move when keyboard is shown")]
		public void ValidateKeyboard_Container_BottomStays()
		{
			ClickBorderSafeAreaButton();
			App.DismissKeyboard();

			App.WaitForElement("SafeAreaContainerButton");
			App.Tap("SafeAreaContainerButton");

			Assert.That(App.FindElement("SafeAreaEdgesValueLabel").GetText(), Is.EqualTo("Container"));

			var insets = GetSafeAreaInsets();
			var (_, screenHeight) = GetScreenSize();

			// ── Before keyboard ──
			var topLabelBeforeRect = App.WaitForElement("TopEdgeIndicator").GetRect();
			Assert.That(Math.Abs(topLabelBeforeRect.Y), Is.EqualTo(insets.Top),
				$"Before keyboard - top label Y ({topLabelBeforeRect.Y}) should be equal to insets.Top ({insets.Top})");

			var bottomLabelBeforeRect = App.WaitForElement("BottomEdgeIndicator").GetRect();
			Assert.That(Math.Abs(bottomLabelBeforeRect.Bottom), Is.EqualTo(screenHeight - insets.Bottom),
				$"Before keyboard - bottom label Bottom ({bottomLabelBeforeRect.Bottom}) should be equal to (screenHeight - insets.Bottom) ({screenHeight - insets.Bottom})");

			Assert.That(App.IsKeyboardShown(), Is.False, "Keyboard should not be visible before tapping entry");
			App.Tap("SafeAreaTestEntry");
			App.WaitForKeyboardToShow();
			Assert.That(App.IsKeyboardShown(), Is.True, "Keyboard should be visible after tapping entry");

#if !ANDROID // On Android, Appium does not find the bottom label when the keyboard is open
			// Bottom should not have moved up to the keyboard top
			var bottomLabelDuringRect = App.WaitForElement("BottomEdgeIndicator").GetRect();
			Assert.That(bottomLabelDuringRect.Bottom, Is.EqualTo(screenHeight - insets.Bottom),
				$"During keyboard - bottom label Bottom ({bottomLabelDuringRect.Bottom}) should equal (screenHeight - insets.Bottom) ({screenHeight - insets.Bottom})");
#endif
			// Top should remain unchanged
			var topLabelDuringRect = App.WaitForElement("TopEdgeIndicator").GetRect();
			Assert.That(topLabelDuringRect.Y, Is.EqualTo(topLabelBeforeRect.Y),
				$"During keyboard - top label Y ({topLabelDuringRect.Y}) should remain at ({topLabelBeforeRect.Y})");

			App.DismissKeyboard();
			App.WaitForKeyboardToHide();
			Assert.That(App.IsKeyboardShown(), Is.False, "Keyboard should be hidden after dismissal");

			// Top should return to its original position
			var topLabelAfterRect = App.WaitForElement("TopEdgeIndicator").GetRect();
			Assert.That(topLabelAfterRect.Y, Is.EqualTo(topLabelBeforeRect.Y),
				$"After keyboard - top label Y ({topLabelAfterRect.Y}) should return to original ({topLabelBeforeRect.Y})");

			// Bottom should return to its original position
			var bottomLabelAfterRect = App.WaitForElement("BottomEdgeIndicator").GetRect();
			Assert.That(bottomLabelAfterRect.Bottom, Is.EqualTo(bottomLabelBeforeRect.Bottom),
				$"After keyboard - bottom label Bottom ({bottomLabelAfterRect.Bottom}) should return to original ({bottomLabelBeforeRect.Bottom})");
		}

		// ──────────────────────────────────────────────
		// Keyboard + Runtime SafeArea Changes
		// ──────────────────────────────────────────────

#if TEST_FAILS_ON_IOS // Issue Link - https://github.com/dotnet/maui/issues/34847

		[Test, Order(15)]
		[Description("Switch None to All while keyboard is open — bottom indicator moves up")]
		public void ValidateKeyboardRuntime_SwitchNoneToAll_WhileKeyboardOpen()
		{
			ClickBorderSafeAreaButton();
			App.DismissKeyboard();

			App.WaitForElement("SafeAreaNoneButton");
			App.Tap("SafeAreaNoneButton");

			var insets = GetSafeAreaInsets();
			var (_, screenHeight) = GetScreenSize();

			// ── Before keyboard (None) ──
			var topLabelBeforeRect = App.WaitForElement("TopEdgeIndicator").GetRect();
			Assert.That(topLabelBeforeRect.Y, Is.EqualTo(0),
				$"Before keyboard - top label Y ({topLabelBeforeRect.Y}) should be 0 (edge-to-edge)");

			var bottomLabelBeforeRect = App.WaitForElement("BottomEdgeIndicator").GetRect();
			Assert.That(Math.Abs(bottomLabelBeforeRect.Bottom), Is.EqualTo(screenHeight),
				$"Before keyboard - bottom label Bottom ({bottomLabelBeforeRect.Bottom}) should be equal to screenHeight ({screenHeight})");

			// ── Show keyboard (None) ──
			Assert.That(App.IsKeyboardShown(), Is.False, "Keyboard should not be visible before tapping entry");
			App.Tap("SafeAreaTestEntry");
			App.WaitForKeyboardToShow();
			Assert.That(App.IsKeyboardShown(), Is.True, "Keyboard should be visible after tapping entry");

			// With None, bottom should NOT move
			var topLabelDuringNoneRect = App.WaitForElement("TopEdgeIndicator").GetRect();
			Assert.That(topLabelDuringNoneRect.Y, Is.EqualTo(0),
				$"During keyboard (None) - top label Y ({topLabelDuringNoneRect.Y}) should be 0 (edge-to-edge)");

#if !ANDROID // On Android, Appium does not find the bottom label when the keyboard is open
			var bottomLabelDuringNoneRect = App.WaitForElement("BottomEdgeIndicator").GetRect();
			Assert.That(Math.Abs(bottomLabelDuringNoneRect.Bottom), Is.EqualTo(screenHeight),
				$"During keyboard (None) - bottom label Bottom ({bottomLabelDuringNoneRect.Bottom}) should be equal to screenHeight ({screenHeight})");
#endif
			// ── Switch to All while keyboard is open ──
			App.Tap("SafeAreaAllButton");
			Assert.That(App.FindElement("SafeAreaEdgesValueLabel").GetText(), Is.EqualTo("All"));

			var keyboardY = GetKeyboardY();

			// With All, bottom should move up to keyboard top
			var topLabelDuringAllRect = App.WaitForElement("TopEdgeIndicator").GetRect();
			Assert.That(Math.Abs(topLabelDuringAllRect.Y), Is.EqualTo(insets.Top),
				$"During keyboard (All) - top label Y ({topLabelDuringAllRect.Y}) should be equal to insets.Top ({insets.Top})");

			var bottomLabelDuringAllRect = App.WaitForElement("BottomEdgeIndicator").GetRect();
			Assert.That(bottomLabelDuringAllRect.Bottom, Is.EqualTo(keyboardY),
				$"During keyboard (All) - bottom label Bottom ({bottomLabelDuringAllRect.Bottom}) should equal keyboard Y ({keyboardY})");

			// ── Dismiss keyboard ──
			App.DismissKeyboard();
			App.WaitForKeyboardToHide();
			Assert.That(App.IsKeyboardShown(), Is.False, "Keyboard should be hidden after dismissal");

			// After keyboard (All): top at insets.Top, bottom at (screenHeight - insets.Bottom)
			var topLabelAfterRect = App.WaitForElement("TopEdgeIndicator").GetRect();
			Assert.That(Math.Abs(topLabelAfterRect.Y), Is.EqualTo(insets.Top),
				$"After keyboard - top label Y ({topLabelAfterRect.Y}) should be equal to insets.Top ({insets.Top})");

			var bottomLabelAfterRect = App.WaitForElement("BottomEdgeIndicator").GetRect();
			Assert.That(Math.Abs(bottomLabelAfterRect.Bottom), Is.EqualTo(screenHeight - insets.Bottom),
				$"After keyboard - bottom label Bottom ({bottomLabelAfterRect.Bottom}) should be equal to (screenHeight - insets.Bottom) ({screenHeight - insets.Bottom})");
		}

		[Test, Order(16)]
		[Description("Switch None to SoftInput while keyboard is open — bottom indicator moves up")]
		public void ValidateKeyboardRuntime_SwitchNoneToSoftInput_WhileKeyboardOpen()
		{
			ClickBorderSafeAreaButton();
			App.DismissKeyboard();

			App.WaitForElement("SafeAreaNoneButton");
			App.Tap("SafeAreaNoneButton");

			var insets = GetSafeAreaInsets();
			var (_, screenHeight) = GetScreenSize();

			// ── Before keyboard (None) ──
			var topLabelBeforeRect = App.WaitForElement("TopEdgeIndicator").GetRect();
			Assert.That(topLabelBeforeRect.Y, Is.EqualTo(0),
				$"Before keyboard - top label Y ({topLabelBeforeRect.Y}) should be 0 (edge-to-edge)");

			var bottomLabelBeforeRect = App.WaitForElement("BottomEdgeIndicator").GetRect();
			Assert.That(Math.Abs(bottomLabelBeforeRect.Bottom), Is.EqualTo(screenHeight),
				$"Before keyboard - bottom label Bottom ({bottomLabelBeforeRect.Bottom}) should be equal to screenHeight ({screenHeight})");

			// ── Show keyboard (None) ──
			Assert.That(App.IsKeyboardShown(), Is.False, "Keyboard should not be visible before tapping entry");
			App.Tap("SafeAreaTestEntry");
			App.WaitForKeyboardToShow();
			Assert.That(App.IsKeyboardShown(), Is.True, "Keyboard should be visible after tapping entry");

			// With None, bottom should NOT move
			var topLabelDuringNoneRect = App.WaitForElement("TopEdgeIndicator").GetRect();
			Assert.That(topLabelDuringNoneRect.Y, Is.EqualTo(0),
				$"During keyboard (None) - top label Y ({topLabelDuringNoneRect.Y}) should be 0 (edge-to-edge)");

#if !ANDROID // On Android, Appium does not find the bottom label when the keyboard is open
			var bottomLabelDuringNoneRect = App.WaitForElement("BottomEdgeIndicator").GetRect();
			Assert.That(Math.Abs(bottomLabelDuringNoneRect.Bottom), Is.EqualTo(screenHeight),
				$"During keyboard (None) - bottom label Bottom ({bottomLabelDuringNoneRect.Bottom}) should be equal to screenHeight ({screenHeight})");
#endif
			// ── Switch to SoftInput while keyboard is open ──
			App.Tap("SafeAreaSoftInputButton");
			Assert.That(App.FindElement("SafeAreaEdgesValueLabel").GetText(), Is.EqualTo("SoftInput"));

			var keyboardY = GetKeyboardY();

			// With SoftInput, bottom should move up to keyboard top
			var topLabelDuringSoftInputRect = App.WaitForElement("TopEdgeIndicator").GetRect();
			Assert.That(Math.Abs(topLabelDuringSoftInputRect.Y), Is.EqualTo(insets.Top),
				$"During keyboard (SoftInput) - top label Y ({topLabelDuringSoftInputRect.Y}) should be equal to insets.Top ({insets.Top})");

			var bottomLabelDuringSoftInputRect = App.WaitForElement("BottomEdgeIndicator").GetRect();
			Assert.That(bottomLabelDuringSoftInputRect.Bottom, Is.EqualTo(keyboardY),
				$"During keyboard (SoftInput) - bottom label Bottom ({bottomLabelDuringSoftInputRect.Bottom}) should equal keyboard Y ({keyboardY})");

			// ── Dismiss keyboard ──
			App.DismissKeyboard();
			App.WaitForKeyboardToHide();
			Assert.That(App.IsKeyboardShown(), Is.False, "Keyboard should be hidden after dismissal");

			// After keyboard (SoftInput): top at insets.Top, bottom at screenHeight (edge-to-edge without keyboard)
			var topLabelAfterRect = App.WaitForElement("TopEdgeIndicator").GetRect();
			Assert.That(Math.Abs(topLabelAfterRect.Y), Is.EqualTo(insets.Top),
				$"After keyboard - top label Y ({topLabelAfterRect.Y}) should be equal to insets.Top ({insets.Top})");

			var bottomLabelAfterRect = App.WaitForElement("BottomEdgeIndicator").GetRect();
			Assert.That(Math.Abs(bottomLabelAfterRect.Bottom), Is.EqualTo(screenHeight),
				$"After keyboard - bottom label Bottom ({bottomLabelAfterRect.Bottom}) should be equal to screenHeight ({screenHeight})");
		}
#endif

		[Test, Order(17)]
		[Description("Switch All to None while keyboard is open — bottom indicator drops back")]
		public void ValidateKeyboardRuntime_SwitchAllToNone_WhileKeyboardOpen()
		{
			ClickBorderSafeAreaButton();
			App.DismissKeyboard();

			// Navigate to Options to reset the ViewModel before the test
			App.WaitForElement("Options");
			App.Tap("Options");

			App.WaitForElement("Apply");
			App.Tap("Apply");

			App.WaitForElement("SafeAreaAllButton");
			App.Tap("SafeAreaAllButton");

			var insets = GetSafeAreaInsets();
			var (_, screenHeight) = GetScreenSize();

			// ── Before keyboard (All) ──
			var topLabelBeforeRect = App.WaitForElement("TopEdgeIndicator").GetRect();
			Assert.That(Math.Abs(topLabelBeforeRect.Y), Is.EqualTo(insets.Top),
				$"Before keyboard - top label Y ({topLabelBeforeRect.Y}) should be equal to insets.Top ({insets.Top})");

			var bottomLabelBeforeRect = App.WaitForElement("BottomEdgeIndicator").GetRect();
			Assert.That(Math.Abs(bottomLabelBeforeRect.Bottom), Is.EqualTo(screenHeight - insets.Bottom),
				$"Before keyboard - bottom label Bottom ({bottomLabelBeforeRect.Bottom}) should be equal to (screenHeight - insets.Bottom) ({screenHeight - insets.Bottom})");

			// ── Show keyboard (All) ──
			Assert.That(App.IsKeyboardShown(), Is.False, "Keyboard should not be visible before tapping entry");
			App.Tap("SafeAreaTestEntry");
			App.WaitForKeyboardToShow();
			Assert.That(App.IsKeyboardShown(), Is.True, "Keyboard should be visible after tapping entry");

			var keyboardY = GetKeyboardY();

			// With All, bottom should move up to keyboard top
			var topLabelDuringAllRect = App.WaitForElement("TopEdgeIndicator").GetRect();
			Assert.That(topLabelDuringAllRect.Y, Is.EqualTo(topLabelBeforeRect.Y),
				$"During keyboard (All) - top label Y ({topLabelDuringAllRect.Y}) should remain at ({topLabelBeforeRect.Y})");

			var bottomLabelDuringAllRect = App.WaitForElement("BottomEdgeIndicator").GetRect();
			Assert.That(bottomLabelDuringAllRect.Bottom, Is.EqualTo(keyboardY),
				$"During keyboard (All) - bottom label Bottom ({bottomLabelDuringAllRect.Bottom}) should equal keyboard Y ({keyboardY})");

			// ── Switch to None while keyboard is open ──
			App.Tap("SafeAreaNoneButton");
			Assert.That(App.FindElement("SafeAreaEdgesValueLabel").GetText(), Is.EqualTo("None"));

			// With None, top goes edge-to-edge; bottom does NOT adjust for keyboard
			var topLabelDuringNoneRect = App.WaitForElement("TopEdgeIndicator").GetRect();
			Assert.That(topLabelDuringNoneRect.Y, Is.EqualTo(0),
				$"During keyboard (None) - top label Y ({topLabelDuringNoneRect.Y}) should be 0 (edge-to-edge)");

#if !ANDROID // On Android, Appium does not find the bottom label when the keyboard is open
			var bottomLabelDuringNoneRect = App.WaitForElement("BottomEdgeIndicator").GetRect();
			Assert.That(Math.Abs(bottomLabelDuringNoneRect.Bottom), Is.EqualTo(screenHeight),
				$"During keyboard (None) - bottom label Bottom ({bottomLabelDuringNoneRect.Bottom}) should be equal to screenHeight ({screenHeight})");
#endif
			// ── Dismiss keyboard ──
			App.DismissKeyboard();
			App.WaitForKeyboardToHide();
			Assert.That(App.IsKeyboardShown(), Is.False, "Keyboard should be hidden after dismissal");

			// After keyboard (None): top at 0, bottom at screenHeight
			var topLabelAfterRect = App.WaitForElement("TopEdgeIndicator").GetRect();
			Assert.That(topLabelAfterRect.Y, Is.EqualTo(0),
				$"After keyboard - top label Y ({topLabelAfterRect.Y}) should be 0 (edge-to-edge)");

			var bottomLabelAfterRect = App.WaitForElement("BottomEdgeIndicator").GetRect();
			Assert.That(Math.Abs(bottomLabelAfterRect.Bottom), Is.EqualTo(screenHeight),
				$"After keyboard - bottom label Bottom ({bottomLabelAfterRect.Bottom}) should be equal to screenHeight ({screenHeight})");
		}

		[Test, Order(18)]
		[Description("Switch Container to SoftInput while keyboard is open — bottom indicator moves up")]
		public void ValidateKeyboardRuntime_SwitchContainerToSoftInput_WhileKeyboardOpen()
		{
			ClickBorderSafeAreaButton();
			App.DismissKeyboard();

			// Navigate to Options to reset the ViewModel before the test
			App.WaitForElement("Options");
			App.Tap("Options");

			App.WaitForElement("Apply");
			App.Tap("Apply");
			App.WaitForElement("SafeAreaContainerButton");
			App.Tap("SafeAreaContainerButton");

			var insets = GetSafeAreaInsets();
			var (_, screenHeight) = GetScreenSize();

			// ── Before keyboard (Container) ──
			var topLabelBeforeRect = App.WaitForElement("TopEdgeIndicator").GetRect();
			Assert.That(Math.Abs(topLabelBeforeRect.Y), Is.EqualTo(insets.Top),
				$"Before keyboard - top label Y ({topLabelBeforeRect.Y}) should be equal to insets.Top ({insets.Top})");

			var bottomLabelBeforeRect = App.WaitForElement("BottomEdgeIndicator").GetRect();
			Assert.That(Math.Abs(bottomLabelBeforeRect.Bottom), Is.EqualTo(screenHeight - insets.Bottom),
				$"Before keyboard - bottom label Bottom ({bottomLabelBeforeRect.Bottom}) should be equal to (screenHeight - insets.Bottom) ({screenHeight - insets.Bottom})");

			// ── Show keyboard (Container) ──
			Assert.That(App.IsKeyboardShown(), Is.False, "Keyboard should not be visible before tapping entry");
			App.Tap("SafeAreaTestEntry");
			App.WaitForKeyboardToShow();
			Assert.That(App.IsKeyboardShown(), Is.True, "Keyboard should be visible after tapping entry");

			// With Container, bottom should NOT move
			var topLabelDuringContainerRect = App.WaitForElement("TopEdgeIndicator").GetRect();
			Assert.That(topLabelDuringContainerRect.Y, Is.EqualTo(topLabelBeforeRect.Y),
				$"During keyboard (Container) - top label Y ({topLabelDuringContainerRect.Y}) should remain at ({topLabelBeforeRect.Y})");

#if !ANDROID // On Android, Appium does not find the bottom label when the keyboard is open
			var bottomLabelDuringContainerRect = App.WaitForElement("BottomEdgeIndicator").GetRect();
			Assert.That(bottomLabelDuringContainerRect.Bottom, Is.EqualTo(screenHeight - insets.Bottom),
				$"During keyboard (Container) - bottom label Bottom ({bottomLabelDuringContainerRect.Bottom}) should equal (screenHeight - insets.Bottom) ({screenHeight - insets.Bottom})");
#endif
			// ── Switch to SoftInput while keyboard is open ──
			App.Tap("SafeAreaSoftInputButton");
			Assert.That(App.FindElement("SafeAreaEdgesValueLabel").GetText(), Is.EqualTo("SoftInput"));

			var keyboardY = GetKeyboardY();

			// With SoftInput, bottom should move up to keyboard top
			var topLabelDuringSoftInputRect = App.WaitForElement("TopEdgeIndicator").GetRect();
			Assert.That(Math.Abs(topLabelDuringSoftInputRect.Y), Is.EqualTo(insets.Top),
				$"During keyboard (SoftInput) - top label Y ({topLabelDuringSoftInputRect.Y}) should be equal to insets.Top ({insets.Top})");

			var bottomLabelDuringSoftInputRect = App.WaitForElement("BottomEdgeIndicator").GetRect();
			Assert.That(bottomLabelDuringSoftInputRect.Bottom, Is.EqualTo(keyboardY),
				$"During keyboard (SoftInput) - bottom label Bottom ({bottomLabelDuringSoftInputRect.Bottom}) should equal keyboard Y ({keyboardY})");

			// ── Dismiss keyboard ──
			App.DismissKeyboard();
			App.WaitForKeyboardToHide();
			Assert.That(App.IsKeyboardShown(), Is.False, "Keyboard should be hidden after dismissal");

			// After keyboard (SoftInput): top at insets.Top, bottom at screenHeight (edge-to-edge without keyboard)
			var topLabelAfterRect = App.WaitForElement("TopEdgeIndicator").GetRect();
			Assert.That(Math.Abs(topLabelAfterRect.Y), Is.EqualTo(insets.Top),
				$"After keyboard - top label Y ({topLabelAfterRect.Y}) should be equal to insets.Top ({insets.Top})");

			var bottomLabelAfterRect = App.WaitForElement("BottomEdgeIndicator").GetRect();
			Assert.That(Math.Abs(bottomLabelAfterRect.Bottom), Is.EqualTo(screenHeight),
				$"After keyboard - bottom label Bottom ({bottomLabelAfterRect.Bottom}) should be equal to screenHeight ({screenHeight})");
		}
	}
}
#endif
