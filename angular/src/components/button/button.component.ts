import {
	ChangeDetectionStrategy,
	Component,
	EventEmitter,
	Input,
	Output,
} from "@angular/core";
import { Action } from "@ngrx/store";

@Component({
	selector: "app-button",
	templateUrl: "./button.component.html",
	styleUrl: "./button.component.scss",
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ButtonComponent {
	@Input()
	public type: "basic" | "raised" | "icon" | "fab" | "flat" | "shopping";

	@Input()
	public label: string;

	@Input()
	public icon: string;

	@Input()
	public action: Action;

	@Input()
	public color: "primary" | "secondary" | "tertiary" = "primary";

	@Output()
	public actionEmmiter = new EventEmitter<Action>();
}
