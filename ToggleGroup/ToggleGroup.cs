using UnityEngine;
using UnityEngine.UIElements;
 
namespace MyUILibrary
{
using System.Collections.Generic;
using UnityEditor;

	public class ToggleGroup : VisualElement{
	 
        protected VisualElement ItemContainer {get;set;}
 
        public override VisualElement contentContainer {
            get => ItemContainer;
        }
        
        Label ToggleLabel;
        
        public ToggleGroup(){
        	this.AddToClassList("toggle-group");
        	this.style.height = new StyleLength(new Length(100,LengthUnit.Percent));
        	
        	ToggleLabel = new Label("MyLabel");
        	ToggleLabel.AddToClassList("toggle-group__top-label");
        	hierarchy.Add(ToggleLabel);
        	
            ItemContainer = new VisualElement();
            ItemContainer.name = "Container";
            ItemContainer.AddToClassList("unity-box");
            ItemContainer.AddToClassList("toggle-grop__container");
            ItemContainer.style.flexDirection = FlexDirection.Row;
            ItemContainer.StretchToParentSize();
            ItemContainer.style.borderBottomLeftRadius = 5;
            ItemContainer.style.borderBottomRightRadius = 5;
            ItemContainer.style.borderTopLeftRadius = 5;
            ItemContainer.style.borderTopRightRadius = 5;
            hierarchy.Add(ItemContainer);
            
            RegisterCallback<AttachToPanelEvent>(evt => {
	        	foreach (var item in ItemContainer.Children())
	        	{
	        		SetVisualElementStyle(item);
	        		item.AddManipulator(new MouseEventLogger());
	        	}
            });
        }
        

        
        public new class UxmlFactory : UxmlFactory<ToggleGroup, UxmlTraits> {}
 
        
        public new class UxmlTraits : VisualElement.UxmlTraits {
            UxmlBoolAttributeDescription m_bool = new UxmlBoolAttributeDescription{ name = "bool", defaultValue = false};
            UxmlIntAttributeDescription m_int = new UxmlIntAttributeDescription{ name = "selectnumber", defaultValue = 0 };
            //UxmlStringAttributeDescription m_string = new UxmlStringAttributeDescription{ name = "selectName"};
            UxmlColorAttributeDescription m_selectcolor = new UxmlColorAttributeDescription{ name = "selectcolor", defaultValue = new Color(0.65f,0.65f,0.65f)};
            UxmlColorAttributeDescription m_defcolor = new UxmlColorAttributeDescription{ name = "defaultcolor", defaultValue = new Color(0.9f,0.9f,0.9f)};
            UxmlStringAttributeDescription m_label = new UxmlStringAttributeDescription{ name = "Label", defaultValue = "" };
            
            public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
	        {
	            get { yield break; }
	        }
            
            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc) {
                base.Init(ve, bag, cc);
                var top = ve as ToggleGroup;
                
                top.selectnumber = m_int.GetValueFromBag(bag,cc);
                
                foreach (var item in top.ItemContainer.Children())
	        	{
	        		top.SetVisualElementStyle(item);
	        		item.AddToClassList("toggle-group__contents");
	        		item.AddManipulator(new MouseEventLogger());
	        	}
                
                top.selectcolor = m_selectcolor.GetValueFromBag(bag,cc);
                top.defaultcolor = m_defcolor.GetValueFromBag(bag,cc);
                
                top.label = m_label.GetValueFromBag(bag, cc);
                top.Q<Label>("","toggle-group__top-label").text = top.label;
            }
        }
	    
	    private bool mouseUp = true;
	    
	    public void SetVisualElementStyle(VisualElement ve)
	    {
	    	ve.style.width = new StyleLength(new Length(100,LengthUnit.Percent));
	    	ve.style.borderBottomLeftRadius = 5;
	    	ve.style.borderBottomRightRadius = 5;
	    	ve.style.borderTopLeftRadius = 5;
	    	ve.style.borderTopRightRadius = 5;
	    	var method = (ItemContainer.IndexOf(ve) == selectnumber) ? SetButtonOnStyle(ve) : SetButtonOffStyle(ve);
	    	ve.style.borderBottomWidth = 1;
	    	ve.style.borderLeftWidth = 1;
	    	ve.style.borderRightWidth = 1;
	    	ve.style.borderTopWidth = 1;
	    	ve.style.marginBottom = new StyleLength(new Length(1,LengthUnit.Percent));
	    	ve.style.marginTop = new StyleLength(new Length(1,LengthUnit.Percent));
	    	ve.style.marginLeft = new StyleLength(new Length(1f,LengthUnit.Percent));
	    	ve.style.marginRight = new StyleLength(new Length(1f,LengthUnit.Percent));
	    	ve.style.paddingBottom = new StyleLength(new Length(1,LengthUnit.Percent));
	    	ve.style.paddingTop = new StyleLength(new Length(1,LengthUnit.Percent));
	    	ve.style.paddingLeft = new StyleLength(new Length(2,LengthUnit.Percent));
	    	ve.style.paddingRight = new StyleLength(new Length(2,LengthUnit.Percent));
	    }
	    
	    public IStyle SetButtonOffStyle(VisualElement ve)
	    {
	    	ve.style.backgroundColor = defaultcolor;
	    	ve.style.borderColor = defaultcolor + Color.white * 0.02f;
	    	ve.style.borderBottomColor = defaultcolor - Color.white * 0.4f;
	    	return ve.style;
	    }
	    
	    public IStyle SetButtonOnStyle(VisualElement ve)
	    {
	    	ve.style.backgroundColor = selectcolor;
	    	ve.style.borderColor = selectcolor - Color.white * 0.2f;
	    	ve.style.borderTopColor = selectcolor + Color.white * 0.1f;
	    	return ve.style;
	    }
	    
	    public IStyle SetButtonDownStyle(VisualElement ve)
	    {
	    	ve.style.backgroundColor = defaultcolor*Color.gray*0.25f;
	    	ve.style.borderColor = defaultcolor*Color.gray*0.25f - Color.white * 0.2f;
	    	ve.style.borderTopColor = defaultcolor*Color.gray*0.25f + Color.white * 0.1f;
	    	return ve.style;
	    }
	    
	    public IStyle SetButtonHoverStyle(VisualElement ve)
	    {
	    	ve.style.backgroundColor = ve.style.backgroundColor.value + Color.white * 0.05f;
	    	ve.style.borderBottomColor = ve.style.borderBottomColor.value + Color.white * 0.09f;
	    	ve.style.borderLeftColor = ve.style.borderLeftColor.value + Color.white * 0.09f;
	    	ve.style.borderRightColor = ve.style.borderRightColor.value + Color.white * 0.09f;
	    	ve.style.borderTopColor = ve.style.borderTopColor.value + Color.white * 0.09f;
	    	return ve.style;
	    }
	    
        class MouseEventLogger : Manipulator
	    {
	    	protected override void RegisterCallbacksOnTarget()
	    	{
	    		target.RegisterCallback<MouseDownEvent>(OnMouseDownEvent);
	    		target.RegisterCallback<MouseEnterEvent>(OnMouseEnterEvent);
	    		target.RegisterCallback<MouseUpEvent>(OnMouseUpEvent);
	    		target.RegisterCallback<MouseOutEvent>(OnMouseOutEvent);
	    	}
	    	
	    	protected override void UnregisterCallbacksFromTarget()
	    	{
	    		target.UnregisterCallback<MouseDownEvent>(OnMouseDownEvent);
	    		target.UnregisterCallback<MouseEnterEvent>(OnMouseEnterEvent);
	    		target.UnregisterCallback<MouseUpEvent>(OnMouseUpEvent);
	    		target.UnregisterCallback<MouseOutEvent>(OnMouseOutEvent);
	    	}
	    	
	    	void OnMouseDownEvent(MouseEventBase<MouseDownEvent> evt)
	    	{
	    		var element = evt.currentTarget as VisualElement;
	    		var top = element.parent as ToggleGroup;
	    		top.mouseUp = false;
	    		foreach (VisualElement item in top.Children())
	    		{
	    			var method = (item.GetHashCode() == element.GetHashCode()) ? top.SetButtonDownStyle(item) : top.SetButtonOffStyle(item);
	    		}
	    	}
	    	
	    	void OnMouseEnterEvent(MouseEventBase<MouseEnterEvent> evt)
	    	{
	    		var element = evt.currentTarget as VisualElement;
	    		var top = element.parent as ToggleGroup;
	    		if (top.mouseUp)
	    			top.SetButtonHoverStyle(element);
	    		else{
		    		foreach (VisualElement item in top.Children())
		    		{
		    			var method = (item.GetHashCode() == element.GetHashCode()) ? top.SetButtonDownStyle(item) : top.SetButtonOffStyle(item);
		    		}
	    		}
	    	}
	    	
	    	void OnMouseOutEvent(MouseEventBase<MouseOutEvent> evt)
	    	{
	    		var element = evt.currentTarget as VisualElement;
	    		var top = element.parent as ToggleGroup;
	    		if (!top.mouseUp) return;
	    		var method = top.IndexOf(element) == top.selectnumber ? top.SetButtonOnStyle(element) : top.SetButtonOffStyle(element);
	    	}
	    	
	    	void OnMouseUpEvent(MouseEventBase<MouseUpEvent> evt)
	    	{
	    		var element = evt.currentTarget as VisualElement;
	    		var top = element.parent as ToggleGroup;
	    		top.selectName = element.name;
	    		top.selectnumber = top.IndexOf(element);
	    		top.mouseUp = true;
	    		foreach (VisualElement item in top.Children())
	    		{
	    			var method = (item.GetHashCode() == element.GetHashCode()) ? top.SetButtonOnStyle(item) : top.SetButtonOffStyle(item);
	    		}
	    	}
	    }
	    
	    public int selectnumber { get; set; }
	    public string label { get; set; }
	    public string selectName { get; set; }
	    public Color selectcolor { get; set; }
	    public Color defaultcolor { get; set; }
	}
}