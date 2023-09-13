using Cinemachine;
using UnityEngine;

public class PlatformVehicle : MonoBehaviour
{
    [SerializeField] private GameObject vehicle;
    [SerializeField] private GameObject triangle;
    [SerializeField] private CinemachineVirtualCamera[] cameras;
    private void Start()
    {
        TriangleTween();
        EventsManager.Instance.ActionModifyVehicle += ModifyVehicle;
    }

    private void OnDestroy()
    {
        EventsManager.Instance.ActionModifyVehicle -= ModifyVehicle;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Vehicle"))
        {
            if (vehicle != null) return;
            triangle.SetActive(false);
            vehicle = other.gameObject;
            EventsManager.Instance.OnCanModifyVehicle(true);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Vehicle"))
        {
            triangle.SetActive(true);
            vehicle = null;
            EventsManager.Instance.OnCanModifyVehicle(false);
        }
    }
    
    private void ModifyVehicle()
    {
        vehicle.transform.rotation = transform.rotation;
        vehicle.transform.position = new Vector3(0,vehicle.transform.position.y,0) + transform.position;
    }

    private void TriangleTween()
    {
        LeanTween.moveY(triangle, triangle.transform.localPosition.y + 1f, .75f).setLoopPingPong(-1);
    }
}
