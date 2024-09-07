import { Meta, moduleMetadata, StoryObj } from "@storybook/angular";
import { ToolbarUiComponent } from "./toolbar-ui.component";
import { createAction } from "@ngrx/store";
import { ToolbarUiModule } from "../toolbar-ui.module";

const mockMainButtonAction = createAction('[Storybook] - Toolbar main button clicked');
const mockProfileButtonAction = createAction('[Storybook] - Toolbar profile button clicked');
const mockSearchButtonAction = createAction('[Storybook] - Toolbar search button clicked');

const toolbar: Meta<ToolbarUiComponent> = {
    title: 'custom-component/toolbar',
    component: ToolbarUiComponent,
    decorators: [
        moduleMetadata({
            imports: [ToolbarUiModule],
        })
    ]
};

export default toolbar;

export type ToolbarStory = StoryObj<ToolbarUiComponent>;

export const BasicToolbarWithMainButtonAndTitle: ToolbarStory = {
    args: {
        id: 'MAIN-STORYBOOK-TOOLBAR',
        mainButton: {
            type: 'icon',
            icon: 'home',
            label: '',
            action: mockMainButtonAction()
        },
        title: 'Storybook',
        toolbarConfig: {
            isTitleSeparete:true,
            isSecondaryToolbar: false
        }
    }
}

export const CompleteToolbar: ToolbarStory = {
    args: {
        id: 'MAIN-STORYBOOK-TOOLBAR',
        mainButton: {
            type: 'icon',
            icon: 'home',
            label: '',
            action: mockMainButtonAction()
        },
        title: 'Storybook',
        secondaryButtons: [
            {
                type: 'basic',
                icon: 'search',
                label: 'Buscar',
                action: mockSearchButtonAction()
            },
            {
                type: 'icon',
                icon: 'account_circle',
                label: '',
                action: mockProfileButtonAction(),
            },
        ],
        toolbarConfig: {
            isTitleSeparete: false,
            isSecondaryToolbar: false
        }
    }
}

export const SecondaryToolbarWithButtons: ToolbarStory = {
    args: {
        id: 'SECONDARY-STORYBOOK-TOOLBAR',
        secondaryButtons: [
            {
                type: 'raised',
                icon: '',
                label: 'Pantalones',
                action: mockSearchButtonAction()
            },
            {
                type: 'raised',
                icon: '',
                label: 'Remeras',
                action: mockMainButtonAction()
            }
        ],
        toolbarConfig: {
            isTitleSeparete: false,
            isSecondaryToolbar: true
        }
    }
}