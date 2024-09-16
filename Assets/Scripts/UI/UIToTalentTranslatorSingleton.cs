using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIToTalentTranslatorSingleton : AbstractSingleton<UIToTalentTranslatorSingleton>
{

    [SerializeField]
    private Button buttonAddTalent;

    [SerializeField]
    private ScrollRect scroller;

    [SerializeField]
    private GameObject displayNoTalentsMessage;

    [Space]

    [SerializeField]
    private UITalentForm prefabTalentForm;

    [SerializeField] //marked readonly for Inspector debugging //TODO ren improve this
    private List<UITalentForm> listTalentForms;

    protected override void Awake()
    {
        base.Awake();

        listTalentForms = new List<UITalentForm>();
        buttonAddTalent.onClick.AddListener(AddNewTalentForm);
    }

    private void Start()
    {
        scroller.normalizedPosition = new Vector2(0, 1);
        displayNoTalentsMessage.SetActive(true);
    }

    public Talent[] GetTalentsFromUIValue()
    { 
        var listTalents = new List<Talent>();

        foreach (var form in listTalentForms)
        {
            listTalents.Add(form.GetTalentFromUI());
        }

        return listTalents.ToArray();
    }

    private void AddNewTalentForm()
    {
        var newForm = Instantiate(prefabTalentForm, scroller.content);

        newForm.RegisterOnRemove(RemoveTalentForm);
        newForm.PopulateDropdowns();
        listTalentForms.Add(newForm);

        scroller.normalizedPosition = new Vector2(0, -1);
        displayNoTalentsMessage.SetActive(false);
    }

    private void RemoveTalentForm(UITalentForm form)
    { 
        form.RemoveSubscriptions();
        listTalentForms.Remove(form);
        Destroy(form.gameObject);

        displayNoTalentsMessage.SetActive(listTalentForms.Count == 0);
    }

}