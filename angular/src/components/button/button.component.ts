import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from "@angular/core";
import { Action } from "@ngrx/store";
import { BehaviorSubject, filter, map, Observable, Subject } from "rxjs";

@Component({
    selector: "app-button",
    templateUrl: "./button.component.html",
    styleUrl: "./button.component.scss"
})
export class ButtonComponent
{
    public destroy$ = new Subject<void>();
    @Input()
    public type: "basic" | "raised" | "icon" | "fab" | "flat" | "shopping";

    @Input()
    public label: string;

    @Input()
    public icon: string;

    @Input()
    public action: Action;

    @Input()
    public color: 'primary' | 'secondary' | 'tertiary' = 'primary';

    @Output()
    public actionEmmiter = new EventEmitter<Action>();
}