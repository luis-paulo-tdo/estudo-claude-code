export interface Category {
  id: string;
  name: string;
  color: string | null;
  isDefault: boolean;
}

export interface CreateCategory {
  name: string;
  color: string | null;
}

export interface UpdateCategory {
  name: string;
  color: string | null;
}
