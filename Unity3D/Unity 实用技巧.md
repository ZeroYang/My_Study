#Unity实用技巧

## 扩展方法

    public static class Extension{
	    public static void ResetTranform(this Transform transform){
		    transform.localPosition = Vector3.zero;
		    transform.localScale = Vector3.one;
		    transform.localRotation = Quaternion.identity;
	    }
    
	    public static Color ChangeAphla(this Color color, float aphla){
		    color.a = aphla;
		    return color;
	    } 
    }
    
    public class TestObject : MonoBehaviour {
	    public Color color;
	    void Awake(){
		    this.gameObject.transform.ResetTranform();
		    color.ChangeAphla(0.4f);
	    }
    }

## RequireComponent

当你添加的一个用了RequireComponent组件的脚本，需要的组件将会自动被添加到game object（游戏物体）。

    [RequireComponent(typeof(Rigidbody))]
    public class People : MonoBehaviour {

## Inspector

###HideInInspector的使用:
在Inspector面板中隐藏public变量

用法：
    
    [HideInInspector]
    
    public Vector3 rotationsPerSecond = new Vector3(0f,0.1f,0f);
    
###TextArea 特性
TextArea 特性可以让我们更加方便的在 Inspector 中编辑字符串文本.

    [TextArea]
    public string backStory;

###Header, Tooltip 和 Space 特性
Header, Tooltip 和 Space 特性可以帮助我们更好的组织 Inspector 中字段的显示.

	[RequireComponent(typeof(Rigidbody))]
	//[HelpURL("http://www.baidu.com")] //不支持HelpURL属性
	[AddComponentMenu("Learning/People")]
	
	public class People : MonoBehaviour {
	    [Header("BaseInfo")]
	    [Multiline(5)]
	    public string name;
	    [Range(-2,2)]
	    public int age;
	
	    [Space(100)]
	    [Tooltip("用于设置性别")]
	    public string sex;
		[ContextMenu("OutputInfo")]
	    void OutputInfo()
	    {
	        print(name+" "+age);
	    }
	}