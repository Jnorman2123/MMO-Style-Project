using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    // Declare variables for the first and third person cameras
    public CinemachineVirtualCamera firstPersonCamera;
    public CinemachineFreeLook thirdPersonCamera;
    // Declare variable for the thirdPersonCamera orbits
    private CinemachineFreeLook.Orbit[] tpcOrbits;
    // Declare variable for the chat Ui window and mouse over chat
    public GameObject chatUIWindow;
    // Set the range and base value of the minimum and maximum orbit scale
    [Range(0.01f, 1f)]
    public float minScale = 0.5f;
    [Range(1F, 5f)]
    public float maxScale = 1;
    // Set the axis state and how much to scale the orbits
    [AxisStateProperty]
    public AxisState zAxis = new AxisState(0, 1, false, true, 50f, 0.1f, 0.1f, "Mouse ScrollWheel", false);
    // Declare variable for first person
    private bool firstPerson;
    // Declare variable for the mouse wheel input, mouse x axis, and mouse y axis
    private float mouseWheelInput;
    private float mouseX;
    private float mouseY;
    // Declare variable for forward input
    private float forwardInput;
    void OnValidate()
    {
        minScale = Mathf.Max(0.01f, minScale);
        maxScale = Mathf.Max(minScale, maxScale);
    }
    void Awake()
    {
        // Set firstPerson to true at start
        firstPerson = true;
        if (thirdPersonCamera != null)
        {
            tpcOrbits = new CinemachineFreeLook.Orbit[thirdPersonCamera.m_Orbits.Length];
            for (int i = 0; i < tpcOrbits.Length; i++)
            {
                tpcOrbits[i].m_Height = thirdPersonCamera.m_Orbits[i].m_Height;
                tpcOrbits[i].m_Radius = thirdPersonCamera.m_Orbits[i].m_Radius;
            }
            SaveDuringPlay.SaveDuringPlay.OnHotSave -= RestoreTPCOrbits;
            SaveDuringPlay.SaveDuringPlay.OnHotSave += RestoreTPCOrbits;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Call the SwitchCamera method
        SwitchCamera();
        // Call the Zoom method
        Zoom();
        // Call the LookUpDown method
        LookUpDown();
        // Call the FreeLook method
        FreeLook();
        // Call the RecenterCamera method
        RecenterCamera();
    }
    // Create a method to switch the active camera
    private void SwitchCamera()
    {
        // Set the mouseWheelInput
        mouseWheelInput = Input.GetAxis("Mouse ScrollWheel");
        if (firstPerson & mouseWheelInput < 0.0f)
        {
            firstPerson = !firstPerson;
        } else if (!firstPerson & thirdPersonCamera.m_Orbits[1].m_Radius <= 5.0f)
        {
            firstPerson = !firstPerson;
        }
        // Set the priorty of each camera based on firstPerson or not
        if(firstPerson)
        {
            firstPersonCamera.Priority = 1;
            thirdPersonCamera.Priority = 0;
        } else
        {
            firstPersonCamera.Priority = 0;
            thirdPersonCamera.Priority = 1;
        }
    }
    // Create method to zoom the thirdPersonCamera in and out
    private void Zoom()
    {
        if (tpcOrbits != null & !firstPerson & !chatUIWindow.GetComponent<ChatWindowController>().mouseOver)
        {
            zAxis.Update(Time.deltaTime);
            float scale = Mathf.Lerp(minScale, maxScale, zAxis.Value);
            for (int i = 0; i < tpcOrbits.Length; i++)
            {
                thirdPersonCamera.m_Orbits[i].m_Height = tpcOrbits[i].m_Height * scale;
                thirdPersonCamera.m_Orbits[i].m_Radius = tpcOrbits[i].m_Radius * scale;
            }
        }
    }
    // Create method to move the camera up and down in first person
    private void LookUpDown()
    {
        // Set mouseY to the mouse y axis
        mouseY = Input.GetAxis("Mouse Y");
        // Check to see if in first person and the right mouse is being held
        if (firstPerson & Input.GetMouseButton(1))
        {
            // Rotate the firstPersonCamera based on the mouseY 
            firstPersonCamera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.Value -= mouseY;     
        } else if (!firstPerson & Input.GetMouseButton(1))
        {
            // Move up and down along the thirdPersonCamera orbit
            thirdPersonCamera.m_YAxis.Value += mouseY/50;
        }
    }
    // Create method to move the thirdPersonCamera around the player
    private void FreeLook()
    {
        // Check to see if in third person and the left mouse button is being held
        if (!firstPerson & Input.GetMouseButton(0) & !chatUIWindow.GetComponent<ChatWindowController>().mouseOver)
        {
            // Set mouseY and mouseX to each axis
            mouseY = Input.GetAxis("Mouse Y")/50;
            mouseX = Input.GetAxis("Mouse X") * 10;
            // Rotate the camera around the player based on the mouseX and mouseY
            thirdPersonCamera.m_YAxis.Value += mouseY;
            thirdPersonCamera.m_XAxis.Value += mouseX;
        } 
    }
    // Create method to recent the third person camera on the player when moving
    private void RecenterCamera()
    {
        // Set the forwardInput to the vertical axis
        forwardInput = Input.GetAxisRaw("Vertical");
        if (!firstPerson & Input.GetMouseButton(1))
        {
            thirdPersonCamera.m_XAxis.Value = 0.0f;
        }
    }
    // Create method to return the tpcOrbits back to original position
    private void RestoreTPCOrbits()
    {
        if (tpcOrbits != null)
        {
            for (int i = 0; i < tpcOrbits.Length; i++)
            {
                thirdPersonCamera.m_Orbits[i].m_Height = tpcOrbits[i].m_Height;
                thirdPersonCamera.m_Orbits[i].m_Radius = tpcOrbits[i].m_Radius;
            }
        }
    }
    // Create method to restore the orginal orbits on scene destroy
    private void OnDestroy()
    {
        SaveDuringPlay.SaveDuringPlay.OnHotSave -= RestoreTPCOrbits;
    }
}















