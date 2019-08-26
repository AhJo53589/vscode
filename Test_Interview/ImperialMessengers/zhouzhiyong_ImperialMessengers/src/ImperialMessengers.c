#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#define UINT_MAX      0xffffffff    // maximum unsigned int value

int getArray(int ***p, int m, int n)
{
	int i;
	*p = (int **)malloc(sizeof(int*) * m);
	memset(*p, 0, sizeof(int*) * m);
	if (p == NULL) return -1;

	for (i = 0; i < n; i++)
	{
		(*p)[i] = (int*)malloc(sizeof(int) * n);
		memset((*p)[i], 0, sizeof(int) * n);
		if ((*p)[i] == NULL) return -1;
	}
	return 0;
}

void freeArray(int **p, int m, int n)
{
	int i = 0;
	for (i = 0; i < n; i++)
	{
		free(p[i]);
		p[i] = NULL;
	}
	free(p);
	p = NULL;
}

void updateTimes(unsigned int *times, int **matrix, int i, int n)
{
	int j = 0;
	for (j = 0; j < n; j++)
	{
		if (matrix[i][j] == 0) break;
		if (matrix[i][j] == -1) continue;
		
		if (j == 0)
		{
			if ((unsigned int)matrix[i][j] < times[i])
			{
				times[i] = (unsigned int)matrix[i][j];
				continue;
			}
		}

		if (times[i] == UINT_MAX)
		{
			if (times[j] == UINT_MAX) continue;
			times[i] = times[j] + (unsigned int)matrix[i][j];
		}
		if (times[j] > times[i] + (unsigned int)matrix[i][j])
		{
			times[j] = times[i] + (unsigned int)matrix[i][j];
			updateTimes(times, matrix, j, n);
		}
	}
}

int main(int argc, char * argv[])
{
	int i = 0;
	int j = 0;

	int n = 0;
	scanf("%d", &n);
	if (n < 1 || n > 100) return 0;

	int **martix = NULL;
	if (getArray(&martix, n, n) == -1) return 0;
	
	unsigned int *times = (unsigned int *)malloc(sizeof(unsigned int) * n);
	memset(times, 0, sizeof(unsigned int) * n);
	if (times == NULL) return 0;

	for (i = 1; i < n; i++)
	{
		times[i] = UINT_MAX;
	}

	for (i = 1; i < n; i++)
	{
		for (j = 0; j < n; j++)
		{
			if (j == i) return 0;	// wrong input format

			char temp[128];
			scanf("%s", &temp);
			if (temp[0] == 'x')
			{
				martix[i][j] = -1;
			}
			else
			{
				martix[i][j] = atoi(temp);
			}
			if (martix[i][j] == 0) return 0;	// wrong input format

			char c;
			if ((c = getchar()) == '\n')
			{
				if (j != i - 1) return 0;	// wrong input format
				break;
			}
		}
		updateTimes(times, martix, i, n);
	}

	//for (i = 0; i < n; i++)
	//{
	//	for (j = 0; j < n; j++)
	//	{
	//		printf("%d\t", martix[i][j]);
	//	}
	//	printf("\n");
	//}

	//for (i = 1; i < n; i++)
	//{
	//	printf("%u, ", times[i]);
	//}
	//printf("\n");

	unsigned int res = 0;
	for (i = 1; i < n; i++)
	{
		if (res < times[i]) res = times[i];
	}
	printf("%u\n", res);

	free(times);
	freeArray(martix, n, n);

	return 0;
}