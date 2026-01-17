export const buildQueryParams = (params: Record<string, unknown>) => {
  const query = new URLSearchParams();

  Object.entries(params).forEach(([key, value]) => {
    if (value === null || value === undefined) return;

    if (Array.isArray(value)) {
      value.forEach((v) => query.append(key, String(v)));
      return;
    }

    query.append(key, String(value));
  });

  return query;
};
