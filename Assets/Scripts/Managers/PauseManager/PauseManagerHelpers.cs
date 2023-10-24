using UnityEngine.UIElements;

public class PauseManagerHelpers {
  public static VisualElement BuildMenuButton(string buttonText) {
    VisualElement pauseButton = new VisualElement();
    pauseButton.AddToClassList("pause-button");

    VisualElement pauseButtonShadow = new VisualElement();
    pauseButtonShadow.AddToClassList("pause-button__shadow");
    pauseButton.Add(pauseButtonShadow);

    VisualElement pauseButtonPress = new VisualElement();
    pauseButtonPress.AddToClassList("pause-button__press");
    pauseButton.Add(pauseButtonPress);

    VisualElement pauseButtonInner = new VisualElement();
    pauseButtonInner.AddToClassList("pause-button__inner");
    pauseButtonPress.Add(pauseButtonInner);

    VisualElement pauseButtonDots = new VisualElement();
    pauseButtonDots.AddToClassList("pause-button__dots");
    pauseButtonInner.Add(pauseButtonDots);

    VisualElement pauseButtonGradient = new VisualElement();
    pauseButtonGradient.AddToClassList("pause-button__gradient");
    pauseButtonInner.Add(pauseButtonGradient);

    VisualElement pauseButtonText = new VisualElement();
    pauseButtonText.AddToClassList("pause-button__text");
    pauseButtonInner.Add(pauseButtonText);

    // Create individual letters
    foreach (char letter in buttonText) {
      Label labelLetter = new Label(letter.ToString());
      labelLetter.AddToClassList("pause-button__letter");
      pauseButtonText.Add(labelLetter);
    }

    VisualElement pauseButtonBorder = new VisualElement();
    pauseButtonBorder.AddToClassList("pause-button__border");
    pauseButtonPress.Add(pauseButtonBorder);

    return pauseButton;
  }

}
