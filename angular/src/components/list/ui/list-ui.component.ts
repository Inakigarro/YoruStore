import { Component, Input } from "@angular/core";
import { Item } from "@root/components/models";
import { Observable } from "rxjs";

@Component({
    selector: 'app-list',
    templateUrl: './list-ui.component.html'
})
export class AppListUiComponent {
    @Input()
    public id: string;

    @Input()
    public title: string;

    @Input()
    public data$: Observable<Item[]>;
}