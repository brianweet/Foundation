using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using EPiServer.SpecializedProperties;
using EPiServer.Web;
using Foundation.Cms.EditorDescriptors;
using System.ComponentModel.DataAnnotations;

namespace Foundation.Cms.Pages
{
    public abstract class FoundationPageData : PageData
    {
        #region Content     

        [CultureSpecific]
        [Display(Name = "Main body", GroupName = SystemTabNames.Content, Order = 100)]
        public virtual XhtmlString MainBody { get; set; }

        [CultureSpecific]
        [Display(Name = "Main content area", GroupName = SystemTabNames.Content, Order = 200)]
        public virtual ContentArea MainContentArea { get; set; }

        #endregion

        #region Metadata

        [CultureSpecific]
        [Display(Name = "Meta Title", GroupName = CmsTabNames.MetaData, Order = 100)]
        public virtual string MetaTitle
        {
            get
            {
                var metaTitle = this.GetPropertyValue(p => p.MetaTitle);

                return !string.IsNullOrWhiteSpace(metaTitle)
                    ? metaTitle
                    : PageName;
            }
            set => this.SetPropertyValue(p => p.MetaTitle, value);
        }

        [CultureSpecific]
        [UIHint(UIHint.Textarea)]
        [Display(GroupName = CmsTabNames.MetaData, Order = 200)]
        public virtual string Keywords { get; set; }

        [CultureSpecific]
        [UIHint(UIHint.Textarea)]
        [Display(Name = "Page description", GroupName = CmsTabNames.MetaData, Order = 300)]
        public virtual string PageDescription { get; set; }

        [CultureSpecific]
        [Display(Name = "Disable indexing", GroupName = CmsTabNames.MetaData, Order = 400)]
        public virtual bool DisableIndexing { get; set; }

        #endregion

        #region Settings

        [CultureSpecific]
        [Display(Name = "Exclude from search",
            Description = "This will determine whether or not to show on search",
            GroupName = SystemTabNames.Settings,
            Order = 100)]
        public virtual bool ExcludeFromSearch { get; set; }

        [CultureSpecific]
        [Display(Name = "Hide site header", GroupName = SystemTabNames.Settings, Order = 200)]
        public virtual bool HideSiteHeader { get; set; }

        [CultureSpecific]
        [Display(Name = "Hide site footer", GroupName = SystemTabNames.Settings, Order = 300)]
        public virtual bool HideSiteFooter { get; set; }

        #endregion

        #region Teaser

        [UIHint(UIHint.Image)]
        [Display(Name = "Page image", GroupName = CmsTabNames.Teaser, Order = 100)]
        public virtual ContentReference PageImage { get; set; }

        [CultureSpecific]
        [UIHint(UIHint.Video)]
        [Display(Name = "Teaser video", GroupName = CmsTabNames.Teaser, Order = 200)]
        public virtual ContentReference TeaserVideo { get; set; }

        [CultureSpecific]
        [UIHint(UIHint.Textarea)]
        [Display(Name = "Teaser text", GroupName = CmsTabNames.Teaser, Order = 300)]
        public virtual string TeaserText
        {
            get
            {
                var teaserText = this.GetPropertyValue(p => p.TeaserText);

                // Use explicitly set teaser text, otherwise fall back to description
                return !string.IsNullOrWhiteSpace(teaserText)
                    ? teaserText
                    : PageDescription;
            }
            set => this.SetPropertyValue(p => p.TeaserText, value);
        }

        [CultureSpecific]
        [SelectOne(SelectionFactoryType = typeof(CalloutContentAlignmentSelectionFactory))]
        [Display(Name = "Teaser text alignment", GroupName = CmsTabNames.Teaser, Order = 400)]
        public virtual string TeaserTextAlignment { get; set; }

        [CultureSpecific]
        [SelectOne(SelectionFactoryType = typeof(TeaserColorThemeSelectionFactory))]
        [Display(Name = "Teaser color theme", GroupName = CmsTabNames.Teaser, Order = 500)]
        public virtual string TeaserColorTheme { get; set; }

        [CultureSpecific]
        [Display(Name = "Teaser button text", GroupName = CmsTabNames.Teaser, Order = 600)]
        public virtual string TeaserButtonText { get; set; }

        [CultureSpecific]
        [SelectOne(SelectionFactoryType = typeof(ButtonBlockStyleSelectionFactory))]
        [Display(Name = "Teaser button style", GroupName = CmsTabNames.Teaser, Order = 700)]
        public virtual string TeaserButtonStyle { get; set; }

        [CultureSpecific]
        [Display(Name = "Display hover effect", GroupName = CmsTabNames.Teaser, Order = 800)]
        public virtual bool ApplyHoverEffect { get; set; }


        [Ignore]
        public string AlignmentCssClass
        {
            get
            {
                string alignmentClass;
                switch (TeaserTextAlignment)
                {
                    case CalloutContentAlignments.Left:
                        alignmentClass = "teaser-content-align--left";
                        break;
                    case CalloutContentAlignments.Right:
                        alignmentClass = "teaser-content-align--right";
                        break;
                    case CalloutContentAlignments.Center:
                        alignmentClass = "teaser-content-align--center";
                        break;
                    default:
                        alignmentClass = string.Empty;
                        break;
                }

                return alignmentClass;
            }
        }

        [Ignore]
        public string ThemeCssClass
        {
            get
            {
                string themeCssClass;
                switch (TeaserColorTheme)
                {
                    case ColorThemes.Light:
                        themeCssClass = "teaser-theme--light";
                        break;
                    case ColorThemes.Dark:
                        themeCssClass = "teaser-theme--dark";
                        break;
                    default:
                        themeCssClass = null;
                        break;
                }

                return themeCssClass;
            }
        }

        #endregion

        #region Styles

        [Display(Name = "CSS files", GroupName = CmsTabNames.Styles, Order = 100)]
        public virtual LinkItemCollection CssFiles { get; set; }

        [Display(GroupName = CmsTabNames.Styles, Order = 200)]
        [UIHint(UIHint.Textarea)]
        public virtual string Css { get; set; }

        #endregion

        #region Scripts

        [Display(Name = "Script files", GroupName = CmsTabNames.Scripts, Order = 100)]
        public virtual LinkItemCollection ScriptFiles { get; set; }

        [UIHint(UIHint.Textarea)]
        [Display(GroupName = CmsTabNames.Scripts, Order = 200)]
        public virtual string Scripts { get; set; }

        #endregion
    }
}