using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionDisplayManager : MonoBehaviour
{

    //List of all insturctions
    [SerializeField]
    List<Sprite> instructionSprites = new List<Sprite>();

    //Where to show instructions
    [SerializeField]
    private GameObject _imageObjectForDisplaying;

    [SerializeField]
    private Image _imageComponent;

    //Index for list
    [SerializeField]
    private int instructionIndex;


    // Start is called before the first frame update
    void Start()
    {
        //Get the object that will display the instructions within the child
        _imageObjectForDisplaying = this.transform.GetChild(0).gameObject;

        if(_imageObjectForDisplaying != null )
        {
            //find the image component
            _imageComponent = _imageObjectForDisplaying.GetComponent<Image>();
            if (_imageComponent != null)
            {
                //For every sprite we find in the subfolder "Resources/Sprites", that is an actual sprite
                foreach (var newSprite in Resources.LoadAll<Sprite>("Sprites"))
                {
                    //Add to the list of instructions
                    instructionSprites.Add(newSprite);
                }

                _imageComponent.sprite = instructionSprites[0];
            }
        }
        
    }

    public void MoveSlideBackwards()
    {
        //We cant go farther back than slide 0
        if (instructionIndex == 0)
            return;
        else
        {
            //decrease the index by 1
            instructionIndex--;
            //load up the next instruciton panel
            _imageComponent.sprite = instructionSprites[instructionIndex];
        }
    }
    public void MoveSlideForwards()
    {
        //We cant go farther than how many insturctions were found
        if (instructionIndex == (instructionSprites.Count - 1))
            return;
        else
        {
            //increase the index by 1
            instructionIndex++;
            //load up the next instruciton panel
            _imageComponent.sprite = instructionSprites[instructionIndex];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
