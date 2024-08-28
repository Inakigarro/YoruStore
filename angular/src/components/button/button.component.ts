import { Component, EventEmitter, Input, Output } from "@angular/core";
import { Action } from "@ngrx/store";

@Component({
    selector: "app-button",
    templateUrl: "./button.component.html",
    styleUrl: "./button.component.scss"
})
export class ButtonComponent
{
    @Input()
    public type: "basic" | "raised" | "icon" | "fab" | "flat";

    @Input()
    public label: string;

    @Input()
    public icon: string;

    @Input()
    public action: Action;

    @Output()
    public actionEmmiter = new EventEmitter<Action>()
}