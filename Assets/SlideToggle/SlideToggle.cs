using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Controls
{
    public class SlideToggle : BaseField<bool>
    {
        public new class UxmlFactory : UxmlFactory<SlideToggle, UxmlTraits> { }
        public new class UxmlTraits : BaseFieldTraits<bool, UxmlBoolAttributeDescription>
        {
        }

        public readonly static new string ussClassName = "slide-toogle";
        public readonly static new string inputUssClassName = "slide-toggle__input";
        public static readonly string inputKnobUssClassName = "slide-toggle__input-knob";
        public static readonly string inputCheckedUssClassName = "slide-toggle__input--checked";

        VisualElement m_Input;
        VisualElement m_knob;

        public SlideToggle() : this(null) { }

        public SlideToggle(string label) : base(label, null)
        {
            AddToClassList(ussClassName);
            m_Input = this.Q(className: BaseField<bool>.inputUssClassName);
            m_Input.AddToClassList(inputUssClassName);
            Add(m_Input);

            m_knob = new VisualElement();
            m_knob.AddToClassList(inputKnobUssClassName);
            m_Input.Add(m_knob);
            RegisterCallback<ClickEvent>(evt => OnClick(evt));
            RegisterCallback<KeyDownEvent>(evt => OnKeydownEvent(evt));
            RegisterCallback<NavigationSubmitEvent>(evt => OnSubmit(evt));
        }

        static void OnSubmit(NavigationSubmitEvent evt)
        {
            SlideToggle slideToggle = evt.currentTarget as SlideToggle;
            slideToggle.ToggleValue();
            evt.StopPropagation();
        }

        static void OnKeydownEvent(KeyDownEvent evt)
        {
            var slideToggle = evt.currentTarget as SlideToggle;

            if (slideToggle.panel?.contextType == ContextType.Player)
            {
                return;
            }
            if (evt.keyCode == KeyCode.KeypadEnter || evt.keyCode == KeyCode.Return || evt.keyCode == KeyCode.Space)
            {
                slideToggle.ToggleValue();
                evt.StopPropagation();
            }
        }

        static void OnClick(ClickEvent evt)
        {
            SlideToggle slideToggle = evt.currentTarget as SlideToggle;
            slideToggle.ToggleValue();
            evt.StopPropagation();
        }

        void ToggleValue()
        {
            value = !value;
        }

        public override void SetValueWithoutNotify(bool newValue)
        {
            base.SetValueWithoutNotify(newValue);

            m_Input.EnableInClassList(inputCheckedUssClassName, newValue);
        }
    }
}

