import { Component, Input } from "@angular/core";
import { Action, Store } from "@ngrx/store";
import { Toolbar } from "@root/components/models";

@Component({
    selector: 'toolbar-feature',
    templateUrl: './toolbar-feature.component.html'
})
export class ToolbarFeatureComponent {
    @Input()
    public toolbar: Toolbar;

    constructor(private store: Store) {}

    public dispatch(action: Action) {
        this.store.dispatch(action);
    }
}