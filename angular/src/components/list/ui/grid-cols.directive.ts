import { Directive, Input, OnInit } from "@angular/core";
import { MatGridList } from "@angular/material/grid-list";
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';

export interface GridColumns {
    xs: number;
    sm: number;
    md: number;
    lg: number;
    xl: number;
}

@Directive({
    selector: '[grid-cols]'
})
export class GridColsDirective implements OnInit {
    private gridCols: GridColumns = {
        xs: 1,
        sm: 2,
        md: 3,
        lg: 4,
        xl: 6,
    }

    public get cols(): GridColumns {
        return this.gridCols;
    }

    @Input()
    public set cols(map: GridColumns) {
        if (map && ('object' === (typeof map))) {
            this.gridCols = map;
        }
    }

    public constructor(
        private grid: MatGridList,
        private breakpointObserver: BreakpointObserver
    ) {
        if (this.grid != null) {
            this.grid.cols = this.gridCols.md
        }
    }

    public ngOnInit(): void {
        if (this.grid != null) {
            this.grid.cols = this.gridCols.md
        }
        this.breakpointObserver.observe([
            Breakpoints.XSmall,
            Breakpoints.Small,
            Breakpoints.Medium,
            Breakpoints.Large,
            Breakpoints.XLarge
        ]).subscribe(result => {
            if(result.breakpoints[Breakpoints.XSmall]){
                this.gridCols.xs
            }
            if (result.breakpoints[Breakpoints.Small]) {
                this.grid.cols = this.gridCols.sm;
            }
            if (result.breakpoints[Breakpoints.Medium]) {
            this.grid.cols = this.gridCols.md;
            }
            if (result.breakpoints[Breakpoints.Large]) {
            this.grid.cols = this.gridCols.lg;
            }
            if (result.breakpoints[Breakpoints.XLarge]) {
            this.grid.cols = this.gridCols.xl;
            }
        });
    }
}