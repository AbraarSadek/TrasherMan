using UnityEngine;

// This script is attached to each trash bin
// It detects when trash collides with the bin and determines if it's correct
public class TrashBinHandler : MonoBehaviour {

    //OnCollisionEnter Method - Called when another collider makes contact with this bin's collider
    private void OnCollisionEnter(Collision collision) {

        GameObject trash = collision.gameObject; //Get the object that collided with the bin

        //If-Statement - Check if the collided object is a valid trash item based on its tag
        if (!IsTrash(trash))
            return;

        //Get tags for both the trash and the bin
        string trashTag = trash.tag;
        string binTag = gameObject.tag;

        //Convert tags into readable names for logging
        string trashName = GetFriendlyTrashName(trashTag);
        string binName = GetFriendlyBinName(binTag);

        //Check if the trash belongs in this bin
        bool isCorrect = IsMatchingBin(trashTag, binTag);

        //If-Else Statement - Log the result and update the score accordingly
        if (isCorrect) {

            Debug.Log($"Correct! {trashName} goes in the {binName}. +10 Points"); //Log correct sorting
            ScoreManager.Instance.AddPoints(10); //Add points for correct sorting

        } else {

            Debug.Log($"Incorrect! {trashName} does not go in the {binName}. -10 Points"); //Log incorrect sorting
            ScoreManager.Instance.AddPoints(-10); //Subtract points for incorrect sorting

        } //End of If-Else Statement

        Destroy(trash); //Remove the trash object from the scene after collision

    } //End of OnCollisionEnter method

    //IsTrash Method - Checks if the collided object is a valid trash item based on its tag
    private bool IsTrash(GameObject obj) {

        if (obj == null) return false;

        string tag = obj.tag;

        //Return true if the tag matches any valid trash category
        return tag == "whatIsGarbage" ||
               tag == "whatIsRecyclable" ||
               tag == "whatIsCompost" ||
               tag == "whatIsElectronics" ||
               tag == "whatIsGlass";

    } //End of IsTrash method

    //IsMatchingBin Method - Determines if the trash tag matches the correct bin tag
    private bool IsMatchingBin(string trashTag, string binTag) {

        //Switch Statement - Check if the trash tag matches the correct bin tag
        switch (trashTag) {

            case "whatIsGarbage":
                return binTag == "whatIsGarbageBin";

            case "whatIsRecyclable":
                return binTag == "whatIsRecycleBin";

            case "whatIsCompost":
                return binTag == "whatIsCompostBin";

            case "whatIsElectronics":
                return binTag == "whatIsElectronicsBin";

            case "whatIsGlass":
                return binTag == "whatIsGlassBin";

            default:
                //If tag is unknown, treat as incorrect
                return false;

        } //End of Switch Statement

    } //End of IsMatchingBin method

    //GetFriendlyTrashName Method - Converts trash tags into user-friendly names for logging
    private string GetFriendlyTrashName(string tag) {

        switch (tag) {

            case "whatIsGarbage": return "Garbage";
            case "whatIsRecyclable": return "Recyclables";
            case "whatIsCompost": return "Compost";
            case "whatIsElectronics": return "Electronics";
            case "whatIsGlass": return "Glass";
            default: return "Unknown Trash";

        } //End of Switch Statement

    } //End of GetFriendlyTrashName method

    //GetFriendlyBinName Method - Converts bin tags into user-friendly names for logging
    private string GetFriendlyBinName(string tag) {

        //Switch Statement - Convert bin tags into user-friendly names for logging
        switch (tag) {

            case "whatIsGarbageBin": return "Garbage Bin";
            case "whatIsRecycleBin": return "Recycle Bin";
            case "whatIsCompostBin": return "Compost Bin";
            case "whatIsElectronicsBin": return "Electronics Bin";
            case "whatIsGlassBin": return "Glass Bin";
            default: return "Unknown Bin";

        } //End of Switch Statement

    } //End of GetFriendlyBinName method

} //End of TrashBinHandler class