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

export interface Item {
    id: string;
    titulo: string;
    descripcion: string;
    precio: number;
}

export interface CrearCategoria {
    nombre: string;
}

export interface Categoria {
    id: string;
    nombre: string;
    items: Item[];
}