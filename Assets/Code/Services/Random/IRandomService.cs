namespace Code.Services.Random
{
  public interface IRandomService
  {
    float Range(float inclusiveMin, float inclusiveMax);
    int Range(int inclusiveMin, int exclusiveMax);
  }
}