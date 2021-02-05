import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Product } from 'src/app/_models/product';
import { baseUrl } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private http: HttpClient) { }

  getProducts():Observable<Product[]>{
    return this.http.get<Product[]>(baseUrl + 'Product');
  }
  getProduct(id: number):Observable<Product>{
    return this.http.get<Product>(baseUrl + 'Product/' + id);
  }
  creatProduct(model: any) {
    return this.http.post(baseUrl + `Product/`, model);
  }

  deleteProduct(params: any) {
    return this.http.delete(baseUrl + `Product/`, {params : params} );
  }
  updateProduct(productParams: any) {
    return this.http.put(baseUrl + `Product/Edit`, productParams);
  }

}
