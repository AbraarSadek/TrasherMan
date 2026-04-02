using UnityEngine;

public class TrashBinHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        GameObject trash = collision.gameObject;

        // Only react if the colliding object is one of our trash types
        if (!IsTrash(trash))
            return;

        string trashTag = trash.tag;
        string binTag = gameObject.tag;

        string trashName = GetFriendlyTrashName(trashTag);
        string binName = GetFriendlyBinName(binTag);

        bool isCorrect = IsMatchingBin(trashTag, binTag);

        if (isCorrect)
        {
            Debug.Log($"Correct! {trashName} goes in the {binName}. +10 Points");
            // TODO: Later add ScoreManager.AddPoints(10);
        }
        else
        {
            Debug.Log($"Incorrect! {trashName} does not go in the {binName}. -10 Points");
            // TODO: Later add ScoreManager.AddPoints(-10);
        }

        // Destroy the trash object
        Destroy(trash);
    }

    private bool IsTrash(GameObject obj)
    {
        if (obj == null) return false;

        string tag = obj.tag;
        return tag == "whatIsGarbage" ||
               tag == "whatIsRecyclable" ||
               tag == "whatIsCompost" ||
               tag == "whatIsElectronics" ||
               tag == "whatIsGlass";
    }

    private bool IsMatchingBin(string trashTag, string binTag)
    {
        switch (trashTag)
        {
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
                return false;
        }
    }

    // Converts tag into nice readable name for logging
    private string GetFriendlyTrashName(string tag)
    {
        switch (tag)
        {
            case "whatIsGarbage": return "Garbage";
            case "whatIsRecyclable": return "Recyclables";
            case "whatIsCompost": return "Compost";
            case "whatIsElectronics": return "Electronics";
            case "whatIsGlass": return "Glass";
            default: return "Unknown Trash";
        }
    }

    // Converts bin tag into nice readable name
    private string GetFriendlyBinName(string tag)
    {
        switch (tag)
        {
            case "whatIsGarbageBin": return "Garbage Bin";
            case "whatIsRecycleBin": return "Recycle Bin"; 
            case "whatIsCompostBin": return "Compost Bin";
            case "whatIsElectronicsBin": return "Electronics Bin";
            case "whatIsGlassBin": return "Glass Bin";
            default: return "Unknown Bin";
        }
    }
}