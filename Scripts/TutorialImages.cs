using UnityEngine;

[System.Serializable]
public class TutorialExamples
{
    [SerializeField]
    private GameObject imageObject;

    public GameObject ImageObject => imageObject;
}

public class TutorialImages : MonoBehaviour
{
    [SerializeField]
    private TutorialChecker tutorialChecker;

    [SerializeField]
    private TutorialExamples[] tutorialExamples, tutorialExamples_WorldMap, tutorialExamples_BasicControls, tutorialExamples_BattleOne, tutorialExamples_BattleTwo;

    [SerializeField]
    private int[] pageIndex, pageIndex_WorldMap, pageIndex_BasicControls, pageIndex_BattleOne, pageIndex_BattleTwo;

    private int imageIndex, currentPage;

    private bool objectEnabled;

    public bool ObjectEnabled => objectEnabled;

    public void CheckTutorialImage()
    {
        for(int i = 0; i < pageIndex.Length; i++)
        {
            if (tutorialChecker.InfoIndex == pageIndex[i])
            {
                tutorialExamples[imageIndex].ImageObject.SetActive(true);

                if(i == 0)
                {
                    for (int j = 0; j < tutorialExamples.Length; j++)
                    {
                        tutorialExamples[j].ImageObject.SetActive(false);
                    }

                    imageIndex = 0;
                    tutorialExamples[imageIndex].ImageObject.SetActive(true);
                    imageIndex++;
                }
                else if(i == pageIndex.Length - 1)
                {
                    for (int j = 0; j < tutorialExamples.Length; j++)
                    {
                        tutorialExamples[j].ImageObject.SetActive(false);
                    }

                    imageIndex = tutorialExamples.Length - 1;
                    tutorialExamples[imageIndex].ImageObject.SetActive(true);
                }
                else
                {
                    if (imageIndex < tutorialExamples.Length)
                    {
                        imageIndex++;
                    }
                }

                currentPage = tutorialChecker.InfoIndex;

                objectEnabled = true;

                return;
            }
            else
            {
                for (int j = 0; j < tutorialExamples.Length; j++)
                {
                    tutorialExamples[j].ImageObject.SetActive(false);
                }

                objectEnabled = false;
            }
        }
    }

    public void CheckTutorialImageMasterMenu(int tutorialIndex)
    {
        switch(tutorialIndex)
        {
            case 0:
                for (int i = 0; i < pageIndex_WorldMap.Length; i++)
                {
                    if (tutorialChecker.InfoIndex == pageIndex_WorldMap[i])
                    {
                        tutorialExamples_WorldMap[imageIndex].ImageObject.SetActive(true);

                        if (i == 0)
                        {
                            for (int j = 0; j < tutorialExamples_WorldMap.Length; j++)
                            {
                                tutorialExamples_WorldMap[j].ImageObject.SetActive(false);
                            }

                            imageIndex = 0;
                            tutorialExamples_WorldMap[imageIndex].ImageObject.SetActive(true);
                            imageIndex++;
                        }
                        else if (i == pageIndex_WorldMap.Length - 1)
                        {
                            for (int j = 0; j < tutorialExamples_WorldMap.Length; j++)
                            {
                                tutorialExamples_WorldMap[j].ImageObject.SetActive(false);
                            }

                            imageIndex = tutorialExamples_WorldMap.Length - 1;
                            tutorialExamples_WorldMap[imageIndex].ImageObject.SetActive(true);
                        }
                        else
                        {
                            if (imageIndex < tutorialExamples_WorldMap.Length)
                            {
                                imageIndex++;
                            }
                        }

                        currentPage = tutorialChecker.InfoIndex;

                        objectEnabled = true;

                        return;
                    }
                    else
                    {
                        for (int j = 0; j < tutorialExamples_WorldMap.Length; j++)
                        {
                            tutorialExamples_WorldMap[j].ImageObject.SetActive(false);
                        }

                        objectEnabled = false;
                    }
                }
                break;
            case 1:
                for (int i = 0; i < pageIndex_BasicControls.Length; i++)
                {
                    if (tutorialChecker.InfoIndex == pageIndex_BasicControls[i])
                    {
                        tutorialExamples_BasicControls[imageIndex].ImageObject.SetActive(true);

                        if (i == 0)
                        {
                            for (int j = 0; j < tutorialExamples_BasicControls.Length; j++)
                            {
                                tutorialExamples_BasicControls[j].ImageObject.SetActive(false);
                            }

                            imageIndex = 0;
                            tutorialExamples_BasicControls[imageIndex].ImageObject.SetActive(true);
                            imageIndex++;
                        }
                        else if (i == pageIndex_BasicControls.Length - 1)
                        {
                            for (int j = 0; j < tutorialExamples_BasicControls.Length; j++)
                            {
                                tutorialExamples_BasicControls[j].ImageObject.SetActive(false);
                            }

                            imageIndex = tutorialExamples_BasicControls.Length - 1;
                            tutorialExamples_BasicControls[imageIndex].ImageObject.SetActive(true);
                        }
                        else
                        {
                            if (imageIndex < tutorialExamples_BasicControls.Length)
                            {
                                imageIndex++;
                            }
                        }

                        currentPage = tutorialChecker.InfoIndex;

                        objectEnabled = true;

                        return;
                    }
                    else
                    {
                        for (int j = 0; j < tutorialExamples_BasicControls.Length; j++)
                        {
                            tutorialExamples_BasicControls[j].ImageObject.SetActive(false);
                        }

                        objectEnabled = false;
                    }
                }
                break;
            case 2:
                for (int i = 0; i < pageIndex_BattleOne.Length; i++)
                {
                    if (tutorialChecker.InfoIndex == pageIndex_BattleOne[i])
                    {
                        tutorialExamples_BattleOne[imageIndex].ImageObject.SetActive(true);

                        if (i == 0)
                        {
                            for (int j = 0; j < tutorialExamples_BattleOne.Length; j++)
                            {
                                tutorialExamples_BattleOne[j].ImageObject.SetActive(false);
                            }

                            imageIndex = 0;
                            tutorialExamples_BattleOne[imageIndex].ImageObject.SetActive(true);
                            imageIndex++;
                        }
                        else if (i == pageIndex_BattleOne.Length - 1)
                        {
                            for (int j = 0; j < tutorialExamples_BattleOne.Length; j++)
                            {
                                tutorialExamples_BattleOne[j].ImageObject.SetActive(false);
                            }

                            imageIndex = tutorialExamples_BattleOne.Length - 1;
                            tutorialExamples_BattleOne[imageIndex].ImageObject.SetActive(true);
                        }
                        else
                        {
                            if (imageIndex < tutorialExamples_BattleOne.Length)
                            {
                                imageIndex++;
                            }
                        }

                        currentPage = tutorialChecker.InfoIndex;

                        objectEnabled = true;

                        return;
                    }
                    else
                    {
                        for (int j = 0; j < tutorialExamples_BattleOne.Length; j++)
                        {
                            tutorialExamples_BattleOne[j].ImageObject.SetActive(false);
                        }

                        objectEnabled = false;
                    }
                }
                break;
            case 3:
                for (int i = 0; i < pageIndex_BattleTwo.Length; i++)
                {
                    if (tutorialChecker.InfoIndex == pageIndex_BattleTwo[i])
                    {
                        tutorialExamples_BattleTwo[imageIndex].ImageObject.SetActive(true);

                        if (i == pageIndex_BattleTwo.Length - 1)
                        {
                            for (int j = 0; j < tutorialExamples_BattleTwo.Length; j++)
                            {
                                tutorialExamples_BattleTwo[j].ImageObject.SetActive(false);
                            }

                            imageIndex = tutorialExamples_BattleTwo.Length - 1;
                            tutorialExamples_BattleTwo[imageIndex].ImageObject.SetActive(true);
                        }
                        else if(i == 0)
                        {
                            for (int j = 0; j < tutorialExamples_BattleTwo.Length; j++)
                            {
                                tutorialExamples_BattleTwo[j].ImageObject.SetActive(false);
                            }

                            imageIndex = 0;
                            tutorialExamples_BattleTwo[imageIndex].ImageObject.SetActive(true);
                        }
                        else if(i == 1)
                        {
                            for (int j = 0; j < tutorialExamples_BattleTwo.Length; j++)
                            {
                                tutorialExamples_BattleTwo[j].ImageObject.SetActive(false);
                            }

                            imageIndex = 1;
                            tutorialExamples_BattleTwo[imageIndex].ImageObject.SetActive(true);
                        }
                        else if(i == 2)
                        {
                            for (int j = 0; j < tutorialExamples_BattleTwo.Length; j++)
                            {
                                tutorialExamples_BattleTwo[j].ImageObject.SetActive(false);
                            }

                            imageIndex = 2;
                            tutorialExamples_BattleTwo[imageIndex].ImageObject.SetActive(true);
                        }

                        currentPage = tutorialChecker.InfoIndex;

                        objectEnabled = true;

                        return;
                    }
                    else
                    {
                        for (int j = 0; j < tutorialExamples_BattleTwo.Length; j++)
                        {
                            tutorialExamples_BattleTwo[j].ImageObject.SetActive(false);
                        }

                        objectEnabled = false;
                    }
                }
                break;
        }
    }

    public void DecrementImageIndex()
    {
        if(tutorialChecker.InfoIndex < currentPage)
        {
            imageIndex--;
            if (imageIndex <= 0)
            {
                imageIndex = 0;
            }
        }
    }

    public void ResetInfo()
    {
        currentPage = 0;
        imageIndex = 0;

        objectEnabled = false;
    }
}