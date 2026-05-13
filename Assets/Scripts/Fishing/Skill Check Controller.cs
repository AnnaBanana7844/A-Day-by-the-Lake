using UnityEngine;
using UnityEngine.UI;

public class SkillCheckController : MonoBehaviour
{
    PlayerController player;

    [Header("UI References")]
    public RectTransform pointer;
    public RectTransform successZone;
   

    [Header("Settings")]
    public float pointerSpeed = 180f;
    public KeyCode inputKey = KeyCode.Space;

    [Header("Pointer Speed Settings")]
    public float basePointerSpeed = 180f;
    public float speedIncreasePerSuccess = 20f;

    private float currentPointerSpeed;

    public FishProgressController progressController;

    private float currentAngle = 0f;
    public bool activeFishing = false;

    // Update is called once per frame
    void Update()
    {
        if (!activeFishing)
            return;
        currentAngle += currentPointerSpeed * Time.deltaTime;
        pointer.localEulerAngles = new Vector3(0,0, -currentAngle);

        if(Input.GetKeyDown(inputKey))
        {
            checkHit();
        }
            
    }

    public void startSkillCheck(float zoneSize, float zonePosition)
    {
        activeFishing = true;

        successZone.localEulerAngles = new Vector3(0, 0, -zonePosition);
        successZone.sizeDelta = new Vector2(zoneSize, successZone.sizeDelta.y);
    }

    private void checkHit()
    {
        float pointerPos = Mathf.Repeat(currentAngle, 360f);
        float zonePos = Mathf.Repeat(-successZone.localEulerAngles.z, 360f);
        float zoneSize = successZone.sizeDelta.x;

        float distance = Mathf.Abs(Mathf.DeltaAngle(pointerPos, zonePos));

        if(distance <= zoneSize / 2f)
        {
            currentPointerSpeed += speedIncreasePerSuccess;
            progressController.skillCheckSuccess();
        }
        else
        {
            progressController.skillCheckFail();
            resetPointerSpeed();
        }
        progressController.triggerNextSkillCheck();
    }

    public void resetPointerSpeed()
    {
        currentPointerSpeed = basePointerSpeed;
    }
}
