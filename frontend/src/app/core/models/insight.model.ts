export type InsightSeverity = 'Info' | 'Warning' | 'Critical';

export interface Insight {
  type: string;
  severity: InsightSeverity;
  message: string;
  categoryId: string | null;
}
