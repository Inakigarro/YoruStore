import { Component, Input } from "@angular/core";
import { ListService } from "../../services/list.service";
import { Action } from "@ngrx/store";
import { Observable } from "rxjs";
import { Item } from "@components/models";

@Component({
    selector: 'list-feature',
    templateUrl: './list-feature.component.html'
})
export class ListFeatureComponent {
    @Input()
    public id: string;

    @Input()
    public titulo: string;

    @Input()
    public data$: Observable<Item[]>;
    constructor(private listService: ListService){}

    public dispatch(action: Action) {
        this.listService.dispatch(action);
    }
}