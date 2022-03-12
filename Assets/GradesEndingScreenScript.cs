using System.Linq;
using GradingSystem;
using UnityEngine;

public class GradesEndingScreenScript : MonoBehaviour
{
    // private SubjectListDisplay subjectListDisplay; 
    // private GradeItemListDisplay gradeItemListDisplay; 
    // // Start is called before the first frame update
    // void Start()
    // {
    //
    //     subjectListDisplay = GameObject.FindGameObjectWithTag("UI_Subjects").GetComponent<SubjectListDisplay>(); 
    //     gradeItemListDisplay = GameObject.FindGameObjectWithTag("UI_Subjects").GetComponent<GradeItemListDisplay>(); 
    //     
    //     foreach (var subjectPointsPair in GradeTracker.GetSubjectsState())
    //     {
    //         subjectListDisplay.AddItem("" + subjectPointsPair.Key);
    //         gradeItemListDisplay.AddItem("" + GradeCalculator.Calculate(subjectPointsPair.Value));
    //     }
    // }


    void Start()
    {
        var gradeSprites = gameObject.GetComponentsInChildren<Transform>().ToList();
        gradeSprites.Remove(this.transform); 
        foreach (var gradeSprite in gradeSprites)
        {
            var subjectName = gradeSprite.name.Substring(0, 3);
            //Debug.Log(gradeSprite.name);
            var gradeScore = GradeTracker.GetSubjectState(subjectName);
            var gradeName = GradeCalculator.Calculate(gradeScore);
            var gradeImages = Resources.LoadAll<Sprite>("Grades");
            var srcGradeSprite = gradeImages.First(s => s.name.ToLower().EndsWith(gradeName.ToString().ToLower()));
            gradeSprite.GetComponent<SpriteRenderer>().sprite = srcGradeSprite;
        }
    }

    //
    // // Update is called once per frame
    // void Update()
    // {
    //     
    // }
}