using NUnit.Framework;
using UITest.Appium;
using UITest.Core;


namespace Microsoft.Maui.TestCases.Tests;

public class ImageFeatureTests : UITest
{
	public const string ImageFeatureMatrix = "Image Feature Matrix";
	public const string Options = "Options";
	public const string Apply = "Apply";
	public const string ImageAspectFit = "ImageAspectFit";
	public const string ImageAspectFill = "ImageAspectFill";
	public const string ImageFill = "ImageFill";
	public const string ImageCenter = "ImageCenter";
	public const string SourceTypeFile = "SourceTypeFile";
	public const string SourceTypeFontImage = "SourceTypeFontImage";
	public const string SourceTypeStream = "SourceTypeStream";
	public const string SourceTypeUri = "SourceTypeUri";
	public const string SourceTypeClipImage = "SourceTypeClipImage";
	public const string IsVisibleFalseRadio = "IsVisibleFalseRadio";
	public const string FlowDirectionRTL = "FlowDirectionRTL";
	public const string ShadowCheckBox = "ShadowCheckBox";
	public const string IsAnimationTrue = "IsAnimationTrue";
	public const string IsAnimationFalse = "IsAnimationFalse";
	public const string RectangleGeometry = "RectangleGeometry";
	public const string EllipseGeometry = "EllipseGeometry";
	public const string RoundRectangleGeometry = "RoundRectangleGeometry";
	public const string LineGeometry = "LineGeometry";
	public const string GeometryGroup = "GeometryGroup";
	public const string LineSegment = "LineSegment";
	public const string ArcSegment = "ArcSegment";
	public const string BezierSegment = "BezierSegment";
	public const string PolyLineSegment = "PolyLineSegment";
	public const string PolyBezierSegment = "PolyBezierSegment";
	public const string QuadraticBezierSegment = "QuadraticBezierSegment";
	public const string PolyQuadraticBezierSegment = "PolyQuadraticBezierSegment";

	public ImageFeatureTests(TestDevice device)
		: base(device)
	{
	}

	protected override void FixtureSetup()
	{
		base.FixtureSetup();
		App.NavigateToGallery(ImageFeatureMatrix);
	}

	[Test]
	[Category(UITestCategories.Image)]
	public void VerifyImageAspect_AspectFitWithImageSourceFromFile()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ImageAspectFit);
		App.Tap(ImageAspectFit);
		App.WaitForElement(SourceTypeFile);
		App.Tap(SourceTypeFile);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("ImageControl");
		VerifyScreenshot();
	}

	[Test]
	[Category(UITestCategories.Image)]
	public void VerifyImageAspect_AspectFitWithImageSourceFromUri()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ImageAspectFit);
		App.Tap(ImageAspectFit);
		App.WaitForElement(SourceTypeUri);
		App.Tap(SourceTypeUri);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("ImageControl", timeout: TimeSpan.FromSeconds(3));
		VerifyScreenshot();
	}
#if TEST_FAILS_ON_ANDROID // Issue Link: https://github.com/dotnet/maui/issues/30576
	[Test]
	[Category(UITestCategories.Image)]
	public void VerifyImageAspect_AspectFitWithImageSourceFromStream()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ImageAspectFit);
		App.Tap(ImageAspectFit);
		App.WaitForElement(SourceTypeStream);
		App.Tap(SourceTypeStream);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("ImageControl");
		VerifyScreenshot();
	}

	[Test]
	[Category(UITestCategories.Image)]
	public void VerifyImageAspect_FillWithImageSourceFromStream()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ImageFill);
		App.Tap(ImageFill);
		App.WaitForElement(SourceTypeStream);
		App.Tap(SourceTypeStream);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("ImageControl");
		VerifyScreenshot();
	}
#endif
	[Test]
	[Category(UITestCategories.Image)]
	public void VerifyImageAspect_AspectFitWithImageSourceFromFontImage()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ImageAspectFit);
		App.Tap(ImageAspectFit);
		App.WaitForElement(SourceTypeFontImage);
		App.Tap(SourceTypeFontImage);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("ImageControl");
		VerifyScreenshot();
	}

#if TEST_FAILS_ON_WINDOWS // Issue Link: https://github.com/dotnet/maui/issues/29812
	[Test]
	[Category(UITestCategories.Image)]
	public void VerifyImageAspect_AspectFillWithImageSourceFromFile()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ImageAspectFill);
		App.Tap(ImageAspectFill);
		App.WaitForElement(SourceTypeFile);
		App.Tap(SourceTypeFile);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("ImageControl");
		VerifyScreenshot();
	}

	[Test]
	[Category(UITestCategories.Image)]
	public void VerifyImageAspect_AspectFillWithImageSourceFromUri()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ImageAspectFill);
		App.Tap(ImageAspectFill);
		App.WaitForElement(SourceTypeUri);
		App.Tap(SourceTypeUri);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("ImageControl", timeout: TimeSpan.FromSeconds(3));
		VerifyScreenshot();
	}
#if TEST_FAILS_ON_ANDROID // Issue Link: https://github.com/dotnet/maui/issues/30576
	[Test]
	[Category(UITestCategories.Image)]
	public void VerifyImageAspect_AspectFillWithImageSourceFromStream()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ImageAspectFill);
		App.Tap(ImageAspectFill);
		App.WaitForElement(SourceTypeStream);
		App.Tap(SourceTypeStream);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("ImageControl");
		VerifyScreenshot();
	}
#endif

	[Test]
	[Category(UITestCategories.Image)]
	public void VerifyImageAspect_AspectFillWithImageSourceFromFontImage()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ImageAspectFill);
		App.Tap(ImageAspectFill);
		App.WaitForElement(SourceTypeFontImage);
		App.Tap(SourceTypeFontImage);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("ImageControl");
		VerifyScreenshot();
	}
#endif

	[Test]
	[Category(UITestCategories.Image)]
	public void VerifyImageAspect_FillWithImageSourceFromFile()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ImageFill);
		App.Tap(ImageFill);
		App.WaitForElement(SourceTypeFile);
		App.Tap(SourceTypeFile);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("ImageControl");
		VerifyScreenshot();
	}

	[Test]
	[Category(UITestCategories.Image)]
	public void VerifyImageAspect_FillWithImageSourceFromUri()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ImageFill);
		App.Tap(ImageFill);
		App.WaitForElement(SourceTypeUri);
		App.Tap(SourceTypeUri);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("ImageControl", timeout: TimeSpan.FromSeconds(3));
		VerifyScreenshot();
	}

	[Test]
	[Category(UITestCategories.Image)]
	public void VerifyImageAspect_FillWithImageSourceFromFontImage()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ImageFill);
		App.Tap(ImageFill);
		App.WaitForElement(SourceTypeFontImage);
		App.Tap(SourceTypeFontImage);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("ImageControl");
		VerifyScreenshot();
	}

	[Test]
	[Category(UITestCategories.Image)]
	public void VerifyImageAspect_CenterWithImageSourceFromFile()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ImageCenter);
		App.Tap(ImageCenter);
		App.WaitForElement(SourceTypeFile);
		App.Tap(SourceTypeFile);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("ImageControl");
		VerifyScreenshot();
	}

	[Test]
	[Category(UITestCategories.Image)]
	public void VerifyImageAspect_CenterWithImageSourceFromUri()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ImageCenter);
		App.Tap(ImageCenter);
		App.WaitForElement(SourceTypeUri);
		App.Tap(SourceTypeUri);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("ImageControl", timeout: TimeSpan.FromSeconds(3));
		VerifyScreenshot();
	}

#if TEST_FAILS_ON_WINDOWS && TEST_FAILS_ON_ANDROID // Issue Link for Windows: https://github.com/dotnet/maui/issues/29813 and for Android: https://github.com/dotnet/maui/issues/30576
	[Test]
	[Category(UITestCategories.Image)]
	public void VerifyImageAspect_CenterWithImageSourceFromStream()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ImageCenter);
		App.Tap(ImageCenter);
		App.WaitForElement(SourceTypeStream);
		App.Tap(SourceTypeStream);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("ImageControl");
		VerifyScreenshot();
	}
#endif

	[Test]
	[Category(UITestCategories.Image)]
	public void VerifyImageAspect_CenterWithImageSourceFromFontImage()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ImageCenter);
		App.Tap(ImageCenter);
		App.WaitForElement(SourceTypeFontImage);
		App.Tap(SourceTypeFontImage);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("ImageControl");
		VerifyScreenshot();
	}

	[Test]
	[Category(UITestCategories.Image)]
	public void VerifyFontImageWithFontColorGreen()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(SourceTypeFontImage);
		App.Tap(SourceTypeFontImage);
		App.WaitForElement("FontColorGreen");
		App.Tap("FontColorGreen");
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("ImageControl");
		VerifyScreenshot();
	}

#if TEST_FAILS_ON_WINDOWS // Issue Link: https://github.com/dotnet/maui/issues/22210
	[Test]
	[Category(UITestCategories.Image)]
	public void VerifyFontImageWithFontSize()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(SourceTypeFontImage);
		App.Tap(SourceTypeFontImage);
		App.WaitForElement("EntryFontSize");
		App.ClearText("EntryFontSize");
		App.Tap("EntryFontSize");
		App.EnterText("EntryFontSize", "100");
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("ImageControl");
		VerifyScreenshot();
	}
#endif

	[Test]
	[Category(UITestCategories.Image)]
	public void VerifyImageWithIsVisibleFalse()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(SourceTypeFile);
		App.Tap(SourceTypeFile);
		App.WaitForElement(IsVisibleFalseRadio);
		App.Tap(IsVisibleFalseRadio);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForNoElement("ImageControl");
	}

#if TEST_FAILS_ON_WINDOWS // Issue Link: https://github.com/dotnet/maui/issues/29812
	[Test]
	[Category(UITestCategories.Image)]
	public void VerifyImageWithShadow()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(SourceTypeFile);
		App.Tap(SourceTypeFile);
		App.WaitForElement(ShadowCheckBox);
		App.Tap(ShadowCheckBox);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("ImageControl");
		VerifyScreenshot();
	}
#endif

	[Test]
	[Category(UITestCategories.Image)]
	public void VerifyImageFlowDirectionRTL()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(SourceTypeFile);
		App.Tap(SourceTypeFile);
		App.WaitForElement(FlowDirectionRTL);
		App.Tap(FlowDirectionRTL);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("ImageControl");
		VerifyScreenshot();
	}

	[Test]
	[Category(UITestCategories.Image)]
	public void VerifyImageWithAnimation()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(IsAnimationTrue);
		App.Tap(IsAnimationTrue);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("ImageControl");
	}

	[Test]
	[Category(UITestCategories.Image)]
	public void VerifyImageWithoutAnimation()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(IsAnimationFalse);
		App.Tap(IsAnimationFalse);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("ImageControl");
	}

	[Test]
	[Category(UITestCategories.Image)]
	public void VerifyImageAspect_FillWithClip_EllipseGeometry()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ImageFill);
		App.Tap(ImageFill);
		App.WaitForElement(SourceTypeClipImage);
		App.Tap(SourceTypeClipImage);
		App.WaitForElement(EllipseGeometry);
		App.Tap(EllipseGeometry);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("ImageControl");
		VerifyScreenshot();
	}

	[Test]
	[Category(UITestCategories.Image)]
	public void VerifyImageAspect_FillWithClip_RectangleGeometry()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ImageFill);
		App.Tap(ImageFill);
		App.WaitForElement(SourceTypeClipImage);
		App.Tap(SourceTypeClipImage);
		App.WaitForElement(RectangleGeometry);
		App.Tap(RectangleGeometry);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("ImageControl");
		VerifyScreenshot();
	}

	[Test]
	[Category(UITestCategories.Image)]
	public void VerifyImageAspect_FillWithClip_RoundedRectangleGeometry()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ImageFill);
		App.Tap(ImageFill);
		App.WaitForElement(SourceTypeClipImage);
		App.Tap(SourceTypeClipImage);
		App.WaitForElement(RoundRectangleGeometry);
		App.Tap(RoundRectangleGeometry);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("ImageControl");
		VerifyScreenshot();
	}

	[Test]
	[Category(UITestCategories.Image)]
	public void VerifyImageAspect_FillWithClip_LineGeometry()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ImageFill);
		App.Tap(ImageFill);
		App.WaitForElement(SourceTypeClipImage);
		App.Tap(SourceTypeClipImage);
		App.WaitForElement(LineGeometry);
		App.Tap(LineGeometry);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("ImageControl");
		VerifyScreenshot();
	}

	[Test]
	[Category(UITestCategories.Image)]
	public void VerifyImageAspect_FillWithClip_GeometryGroup()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ImageFill);
		App.Tap(ImageFill);
		App.WaitForElement(SourceTypeClipImage);
		App.Tap(SourceTypeClipImage);
		App.WaitForElement(GeometryGroup);
		App.Tap(GeometryGroup);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("ImageControl");
		VerifyScreenshot();
	}

	[Test]
	[Category(UITestCategories.Image)]
	public void VerifyImageAspect_FillWithClip_PathGeometry_LineSegment()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ImageFill);
		App.Tap(ImageFill);
		App.WaitForElement(SourceTypeClipImage);
		App.Tap(SourceTypeClipImage);
		App.WaitForElement(LineSegment);
		App.Tap(LineSegment);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("ImageControl");
		VerifyScreenshot();
	}

	[Test]
	[Category(UITestCategories.Image)]
	public void VerifyImageAspect_FillWithClip_PathGeometry_ArcSegment()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ImageFill);
		App.Tap(ImageFill);
		App.WaitForElement(SourceTypeClipImage);
		App.Tap(SourceTypeClipImage);
		App.WaitForElement(ArcSegment);
		App.Tap(ArcSegment);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("ImageControl");
		VerifyScreenshot();
	}

	[Test]
	[Category(UITestCategories.Image)]
	public void VerifyImageAspect_FillWithClip_PathGeometry_BezierSegment()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ImageFill);
		App.Tap(ImageFill);
		App.WaitForElement(SourceTypeClipImage);
		App.Tap(SourceTypeClipImage);
		App.WaitForElement(BezierSegment);
		App.Tap(BezierSegment);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("ImageControl");
		VerifyScreenshot();
	}

	[Test]
	[Category(UITestCategories.Image)]
	public void VerifyImageAspect_FillWithClip_PathGeometry_PolyLineSegment()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ImageFill);
		App.Tap(ImageFill);
		App.WaitForElement(SourceTypeClipImage);
		App.Tap(SourceTypeClipImage);
		App.WaitForElement(PolyLineSegment);
		App.Tap(PolyLineSegment);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("ImageControl");
		VerifyScreenshot();
	}

	[Test]
	[Category(UITestCategories.Image)]
	public void VerifyImageAspect_FillWithClip_PathGeometry_PolyBezierSegment()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ImageFill);
		App.Tap(ImageFill);
		App.WaitForElement(SourceTypeClipImage);
		App.Tap(SourceTypeClipImage);
		App.WaitForElement(PolyBezierSegment);
		App.Tap(PolyBezierSegment);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("ImageControl");
		VerifyScreenshot();
	}

	[Test]
	[Category(UITestCategories.Image)]
	public void VerifyImageAspect_FillWithClip_PathGeometry_QuadraticBezierSegment()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ImageFill);
		App.Tap(ImageFill);
		App.WaitForElement(SourceTypeClipImage);
		App.Tap(SourceTypeClipImage);
		App.WaitForElement(QuadraticBezierSegment);
		App.Tap(QuadraticBezierSegment);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("ImageControl");
		VerifyScreenshot();
	}

	[Test]
	[Category(UITestCategories.Image)]
	public void VerifyImageAspect_FillWithClip_PathGeometry_PolyQuadraticBezierSegment()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ImageFill);
		App.Tap(ImageFill);
		App.WaitForElement(SourceTypeClipImage);
		App.Tap(SourceTypeClipImage);
		App.WaitForElement(PolyQuadraticBezierSegment);
		App.Tap(PolyQuadraticBezierSegment);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("ImageControl");
		VerifyScreenshot();
	}

	[Test]
	[Category(UITestCategories.Image)]
	public void VerifyImageAspect_FillFromUriWithClip_EllipseGeometry()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ImageFill);
		App.Tap(ImageFill);
		App.WaitForElement(SourceTypeUri);
		App.Tap(SourceTypeUri);
		App.WaitForElement(EllipseGeometry);
		App.Tap(EllipseGeometry);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("ImageControl");
		VerifyScreenshot();
	}

	[Test]
	[Category(UITestCategories.Image)]
	public void VerifyImageAspect_FillFromUriWithClip_RectangleGeometry()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ImageFill);
		App.Tap(ImageFill);
		App.WaitForElement(SourceTypeUri);
		App.Tap(SourceTypeUri);
		App.WaitForElement(RectangleGeometry);
		App.Tap(RectangleGeometry);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("ImageControl");
		VerifyScreenshot();
	}

	[Test]
	[Category(UITestCategories.Image)]
	public void VerifyImageAspect_FillFromUriWithClip_RoundedRectangleGeometry()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ImageFill);
		App.Tap(ImageFill);
		App.WaitForElement(SourceTypeUri);
		App.Tap(SourceTypeUri);
		App.WaitForElement(RoundRectangleGeometry);
		App.Tap(RoundRectangleGeometry);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("ImageControl");
		VerifyScreenshot();
	}

	[Test]
	[Category(UITestCategories.Image)]
	public void VerifyImageAspect_FillFromUriWithClip_LineGeometry()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ImageFill);
		App.Tap(ImageFill);
		App.WaitForElement(SourceTypeUri);
		App.Tap(SourceTypeUri);
		App.WaitForElement(LineGeometry);
		App.Tap(LineGeometry);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("ImageControl");
		VerifyScreenshot();
	}

	[Test]
	[Category(UITestCategories.Image)]
	public void VerifyImageAspect_FillFromUriWithClip_GeometryGroup()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ImageFill);
		App.Tap(ImageFill);
		App.WaitForElement(SourceTypeUri);
		App.Tap(SourceTypeUri);
		App.WaitForElement(GeometryGroup);
		App.Tap(GeometryGroup);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("ImageControl");
		VerifyScreenshot();
	}

	[Test]
	[Category(UITestCategories.Image)]
	public void VerifyImageAspect_FillFromUriWithClip_PathGeometry_LineSegment()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ImageFill);
		App.Tap(ImageFill);
		App.WaitForElement(SourceTypeUri);
		App.Tap(SourceTypeUri);
		App.WaitForElement(LineSegment);
		App.Tap(LineSegment);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("ImageControl");
		VerifyScreenshot();
	}

	[Test]
	[Category(UITestCategories.Image)]
	public void VerifyImageAspect_FillFromUriWithClip_PathGeometry_ArcSegment()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ImageFill);
		App.Tap(ImageFill);
		App.WaitForElement(SourceTypeUri);
		App.Tap(SourceTypeUri);
		App.WaitForElement(ArcSegment);
		App.Tap(ArcSegment);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("ImageControl");
		VerifyScreenshot();
	}

	[Test]
	[Category(UITestCategories.Image)]
	public void VerifyImageAspect_FillFromUriWithClip_PathGeometry_BezierSegment()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ImageFill);
		App.Tap(ImageFill);
		App.WaitForElement(SourceTypeUri);
		App.Tap(SourceTypeUri);
		App.WaitForElement(BezierSegment);
		App.Tap(BezierSegment);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("ImageControl");
		VerifyScreenshot();
	}

	[Test]
	[Category(UITestCategories.Image)]
	public void VerifyImageAspect_FillFromUriWithClip_PathGeometry_PolyLineSegment()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ImageFill);
		App.Tap(ImageFill);
		App.WaitForElement(SourceTypeUri);
		App.Tap(SourceTypeUri);
		App.WaitForElement(PolyLineSegment);
		App.Tap(PolyLineSegment);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("ImageControl");
		VerifyScreenshot();
	}

	[Test]
	[Category(UITestCategories.Image)]
	public void VerifyImageAspect_FillFromUriWithClip_PathGeometry_PolyBezierSegment()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ImageFill);
		App.Tap(ImageFill);
		App.WaitForElement(SourceTypeUri);
		App.Tap(SourceTypeUri);
		App.WaitForElement(PolyBezierSegment);
		App.Tap(PolyBezierSegment);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("ImageControl");
		VerifyScreenshot();
	}

	[Test]
	[Category(UITestCategories.Image)]
	public void VerifyImageAspect_FillFromUriWithClip_PathGeometry_QuadraticBezierSegment()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ImageFill);
		App.Tap(ImageFill);
		App.WaitForElement(SourceTypeUri);
		App.Tap(SourceTypeUri);
		App.WaitForElement(QuadraticBezierSegment);
		App.Tap(QuadraticBezierSegment);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("ImageControl");
		VerifyScreenshot();
	}

	[Test]
	[Category(UITestCategories.Image)]
	public void VerifyImageAspect_FillFromUriWithClip_PathGeometry_PolyQuadraticBezierSegment()
	{
		App.WaitForElement(Options);
		App.Tap(Options);
		App.WaitForElement(ImageFill);
		App.Tap(ImageFill);
		App.WaitForElement(SourceTypeUri);
		App.Tap(SourceTypeUri);
		App.WaitForElement(PolyQuadraticBezierSegment);
		App.Tap(PolyQuadraticBezierSegment);
		App.WaitForElement(Apply);
		App.Tap(Apply);
		App.WaitForElement("ImageControl");
		VerifyScreenshot();
	}
}