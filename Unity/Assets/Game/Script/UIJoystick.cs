using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets.Vehicles.Aeroplane;

public class UIJoystick : MonoBehaviour, IDragHandler, IEndDragHandler
{
	/// <summary>
	/// 被用户拖动的操纵杆
	/// </summary>
	public Transform target;

	/// <summary>
	/// 操纵杆可移动的最大半径
	/// </summary>
	public float radius = 50f;

	/// <summary>
	/// 当前操纵杆在2D空间的x,y位置
	/// 摇杆按钮的值【-1，1】之间
	/// </summary>
	public Vector2 position;

	////操纵杆的RectTransform组件
	//private RectTransform thumb;

	void Start()
	{
		//thumb = target.GetComponent<RectTransform>();
	}

	/// <summary>
	/// 当操纵杆被拖动时触发
	/// </summary>
	public void OnDrag(PointerEventData data)
	{
        CameraManager.swipeLock = 0;
		//获取摇杆的RectTransform组件，以检测操纵杆是否在摇杆内移动
		RectTransform draggingPlane = transform as RectTransform;
		Vector3 mousePos;

		//检查拖动的位置是否在拖动rect内，
		//然后设置全局鼠标位置并将其分配给操纵杆
		if (RectTransformUtility.ScreenPointToWorldPointInRectangle(draggingPlane, data.position, data.pressEventCamera, out mousePos))
		{
			target.position = mousePos;
		}

		//触摸向量的长度（大小）
		//计算操作杆的相对位置
		float length = target.localPosition.magnitude;

		//如果操纵杆超过了摇杆的范围，则将操纵杆设置为最大半径
		if (length > radius)
		{
			target.localPosition = Vector3.ClampMagnitude(target.localPosition, radius);
		}

		JoyStickInput.Direction = target.position - transform.position;

		//在Inspector显示操纵杆位置
		position = target.localPosition;
		//将操纵杆相对位置映射到【-1，1】之间
		position = position / radius * Mathf.InverseLerp(radius, 2, 1);
	}
	/// <summary>
	/// 当操纵杆结束拖动时触发
	/// </summary>
	public void OnEndDrag(PointerEventData data)
	{
		//拖拽结束，将操纵杆恢复到默认位置
		position = Vector2.zero;
		target.position = transform.position;
		JoyStickInput.Direction = new Vector2(0, 0);
	}
}