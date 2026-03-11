using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Packaging;

[Weight(0)]
public class MigrationPlan : PackageMigrationPlan
{
    public MigrationPlan() : base("Shout.Migration.Example")
    {
    }

    public override bool IgnoreCurrentState => false;

    protected override void DefinePlan()
    {
        From(string.Empty);
        To<UpdateEditorUiAliasMigration>("update-editor-ui-alias-migration");
        To<UpdateBlockElementTitleMigration>("update-block-element-title-migration");
    }
}