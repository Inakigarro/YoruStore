import { Action } from "@ngrx/store";

export interface Button {
    type: 'basic' | 'raised' | 'fab' | 'icon' | 'flat';
    label: string;
    icon: string;
    action: Action;
}

export interface Toolbar {
    id: string;
    mainButton?: Button;
    title?: string;
    secondaryButton?: Button[];
    toolbarConfig: ToolbarConfig;
}

export interface ToolbarConfig {
    isTitleSeparete: boolean;
    isSecondaryToolbar: boolean;
}