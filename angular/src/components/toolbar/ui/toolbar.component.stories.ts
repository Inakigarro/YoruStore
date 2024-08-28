import { Meta, moduleMetadata, StoryObj } from "@storybook/angular";
import { AppToolbarComponent } from "./toolbar.component";
import { AppToolbarModule } from "../toolbar.module";
import { createAction } from "@ngrx/store";

const mockMainButtonAction = createAction('[Storybook] - Toolbar main button clicked');
const mockProfileButtonAction = createAction('[Storybook] - Toolbar profile button clicked');
const mockSearchButtonAction = createAction('[Storybook] - Toolbar search button clicked');

const toolbar: Meta<AppToolbarComponent> = {
    title: 'custom-component/toolbar',
    component: AppToolbarComponent,
    decorators: [
        moduleMetadata({
            imports: [AppToolbarModule]
        })
    ]
};

export default toolbar;

export type ToolbarStory = StoryObj<AppToolbarComponent>;

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