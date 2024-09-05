import { Component, Input } from "@angular/core";
import { Action, Store } from "@ngrx/store";
import { Item } from "@components/models";

@Component({
    selector: 'app-card-feature',
    templateUrl: './card-feature.component.html',
    styleUrl: './card-feature.component.scss'
})
export class CardFeatureComponent {
    @Input()
    public item: Item;

    constructor(private store: Store){}

    public dispatch(action: Action){
        this.store.dispatch(action);
    }
}