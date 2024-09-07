import { Component, EventEmitter, Input, Output } from "@angular/core";
import { Action } from "@ngrx/store";
import { Button, ToolbarConfig } from "@root/components/models";

@Component({
    selector: 'app-toolbar-ui',
    templateUrl: './toolbar-ui.component.html',
    styleUrl: './toolbar-ui.component.scss'
})
export class ToolbarUiComponent {
    @Input()
    public id: string;
    
    @Input()
    public mainButton: Button | undefined;

    @Input()
    public title: string | undefined;

    @Input()
    public secondaryButtons: Button[] | undefined;

    @Input()
    public toolbarConfig: ToolbarConfig;

    @Output()
    public actionEmitter = new EventEmitter<Action>();
}