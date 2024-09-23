import { Component, OnDestroy, OnInit } from "@angular/core";
import { CategoriasService } from "../categorias.service";
import { Observable, Subject } from "rxjs";
import { Item } from "@root/components/models";

@Component({
	selector: "categorias-list",
	templateUrl: "./categorias-list.component.html",
})
export class CategoriasListComponent implements OnInit, OnDestroy {
	private destroy$ = new Subject<void>();
	public listTitle$: Observable<string>;
	public listData$: Observable<Item[]>;
	constructor(private service: CategoriasService) {}

	public ngOnInit(): void {
		this.listData$ = this.service.data$;
		this.listTitle$ = this.service.title$;
	}

	public ngOnDestroy(): void {
		this.destroy$.next();
		this.destroy$.complete();
	}
}
